using Mario.Application.Services;
using Mario.Game.Enums;
using Mario.Game.ScriptableObjects.Map;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private PlayerSoundFX _playerSoundFX;
        [SerializeField] private SpriteRenderer _render;
        [Header("RayCast")]
        [SerializeField] private GameObject raycastRangesBig;
        [SerializeField] private GameObject raycastRangesSmall;

        private PlayerProfile _profile;
        private Bounds<bool> _proximityBlock = new Bounds<bool>();
        private Vector2 _currentSpeed;
        private float _lastJumpPressed = 0;
        private PlayerModes _mode;
        private bool _isDucking;
        #endregion

        #region Properties
        public PlayerInput Input { get; private set; }
        public Vector3 RawMovement { get; private set; }
        public PlayerModes Mode
        {
            get => _mode;
            private set
            {
                if (_mode == value)
                    return;

                AllServices.PlayerService.CurrentMode = _mode = value;
            }
        }

        public float WalkSpeedFactor => Mathf.Abs(_currentSpeed.x) / _profile.Walk.MaxSpeed;
        public bool IsGrounded => _proximityBlock.bottom;
        public bool IsJumping { get; private set; }
        public bool IsDucking
        {
            get => _isDucking;
            private set
            {
                if (_isDucking == value)
                    return;

                _isDucking = value;
                if (value)
                    SetDuckingCollider();
                else
                    SetModeCollider(_mode);
            }
        }
        public bool IsStuck { get; set; }
        public bool IsDead { get; private set; }
        public bool IsAutoWalk { get; set; }
        public bool IsInvensible { get; set; }

        private bool JumpMinBuffered => _lastJumpPressed + _profile.Jump.MinBufferTime > Time.time;
        private bool JumpMaxBuffered
        {
            get
            {
                float absCurrentSpeed = Mathf.Abs(_currentSpeed.x);
                if (absCurrentSpeed > _profile.Walk.MaxSpeed)
                {
                    float speedFactor = Mathf.InverseLerp(_profile.Walk.MaxSpeed, _profile.Run.MaxSpeed, absCurrentSpeed);
                    float finalBufferTime = Mathf.Lerp(_profile.Jump.MaxWalkBufferTime, _profile.Jump.MaxRunBufferTime, speedFactor);
                    return _lastJumpPressed + finalBufferTime > Time.time;
                }
                else
                    return _lastJumpPressed + _profile.Jump.MaxWalkBufferTime > Time.time;
            }
        }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            AllServices.TimeService.OnTimeOut.AddListener(OnTimeOut);
            AllServices.PlayerService.CanMove = false;

            _profile = AllServices.GameDataService.PlayerProfile;
            Input = new PlayerInput();
            transform.position = AllServices.GameDataService.CurrentMapProfile.MapInit.StartPosition;

            _mode = AllServices.PlayerService.CurrentMode;
            SetModeCollider(_mode);
        }
        private void OnDestroy()
        {
            AllServices.TimeService.OnTimeOut.RemoveListener(OnTimeOut);
        }
        private void Update()
        {
            if (!AllServices.PlayerService.CanMove)
                return;

            GatherInput();

            CalculateWalk();
            CalculateGravity();
            CalculateJump();
            MoveCharacter();
        }
        #endregion

        #region Update Methods
        private void GatherInput()
        {
            if (IsAutoWalk)
            {
                Input = new PlayerInput
                {
                    X = 1,
                };
                return;
            }

            var _jumpDown = Input.JumpDown;
            Input = new PlayerInput
            {
                JumpDown = UnityEngine.Input.GetKey(KeyCode.X),
                X = UnityEngine.Input.GetAxisRaw("Horizontal"),
                Run = UnityEngine.Input.GetKey(KeyCode.Z),
                IsDucking = UnityEngine.Input.GetKey(KeyCode.DownArrow),
            };

            CalculateJump(_jumpDown);
        }
        private void CalculateJump(bool prevJumpState)
        {
            if (IsGrounded && !prevJumpState && Input.JumpDown)
            {
                IsJumping = true;
                _lastJumpPressed = Time.time;

                if (this.Mode == PlayerModes.Small)
                    _playerSoundFX.PlayJumpSmall();
                else
                    _playerSoundFX.PlayJumpBig();
            }
        }
        private void CalculateWalk()
        {
            this.IsDucking = Mode != PlayerModes.Small && Input.X == 0 && Input.IsDucking;

            if (Input.X != 0 && !Input.IsDucking)
            {
                float currentAcceleration = Input.Run ? _profile.Run.Acceleration : _profile.Walk.Acceleration;
                _currentSpeed.x += Input.X * currentAcceleration * Time.deltaTime;

                float _speed = Input.Run ? _profile.Run.MaxSpeed : _profile.Walk.MaxSpeed;
                _currentSpeed.x = Mathf.Clamp(_currentSpeed.x, -_speed, _speed);
            }

            if (RawMovement.x != 0 && (Input.X == 0 || Mathf.Sign(RawMovement.x) != Mathf.Sign(Input.X) || Input.IsDucking))
            {
                float currentDeacceleration = Input.Run ? _profile.Run.Deacceleration : _profile.Walk.Deacceleration;
                _currentSpeed.x = Mathf.MoveTowards(_currentSpeed.x, 0, currentDeacceleration * Time.deltaTime);
            }

            if (_currentSpeed.x > 0 && _proximityBlock.right || _currentSpeed.x < 0 && _proximityBlock.left)
                _currentSpeed.x = 0; // Don't walk through walls
        }
        private void CalculateGravity()
        {
            _currentSpeed.y -= _profile.Fall.FallSpeed * Time.deltaTime;
            if (IsGrounded)
            {
                if (_currentSpeed.y < 0)
                    _currentSpeed.y = 0;
            }
            else
            {
                if (_currentSpeed.y < -_profile.Fall.MaxFallSpeed)
                    _currentSpeed.y = -_profile.Fall.MaxFallSpeed;
            }
        }
        private void CalculateJump()
        {
            if (IsAutoWalk)
            {
                IsJumping = false;
                if (_currentSpeed.y > 0)
                    _currentSpeed.y = 0;

                return;
            }

            if (IsJumping) // evita retomar la aceleracion del salto despues que este empezo a caer
            {
                if (JumpMinBuffered || (Input.JumpDown && JumpMaxBuffered))
                    _currentSpeed.y += _profile.Jump.Acceleration * Time.deltaTime;
                else
                    IsJumping = false;
            }

            if (_currentSpeed.y > _profile.Jump.MaxSpeed)
                _currentSpeed.y = _profile.Jump.MaxSpeed;

            if (_proximityBlock.top)
            {
                if (_currentSpeed.y > 0)
                {
                    _currentSpeed.y = 0;
                    _lastJumpPressed = 0;
                }
            }
        }
        private void MoveCharacter()
        {
            RawMovement = _currentSpeed;
            var nextPosition = transform.position + RawMovement * Time.deltaTime;

            // ajusto posicion de contacto con el suelo
            if (IsGrounded && RawMovement.y == 0)
                nextPosition.y = Mathf.Round(nextPosition.y);

            // fuerzo ajuste de posicion en los lados de los bloques 
            if (!IsGrounded || IsStuck)
                nextPosition = AdjustHorizontalPosition(nextPosition);

            transform.position = nextPosition;
            IsStuck = false;

            AllServices.PlayerService.Position = transform.position;
        }
        private Vector3 AdjustHorizontalPosition(Vector3 position)
        {
            if (!_proximityBlock.left && _proximityBlock.right)
                position.x -= _profile.Jump.HorizontalAdjustmentSpeed * Time.deltaTime;
            else if (!_proximityBlock.right && _proximityBlock.left)
                position.x += _profile.Jump.HorizontalAdjustmentSpeed * Time.deltaTime;
            else if (_proximityBlock.right && _proximityBlock.left)
                position.x += _profile.Jump.HorizontalAdjustmentSpeed * Time.deltaTime;

            return position;
        }
        private void SetSmallCollider()
        {
            _render.transform.localPosition = _profile.SpritePositions.Small;
            EnableRaycastRange(enableSmall: true);
        }
        private void SetBigCollider()
        {
            _render.transform.localPosition = _profile.SpritePositions.Big;
            EnableRaycastRange(enableBig: true);
        }
        private void SetDuckingCollider()
        {
            _render.transform.localPosition = _profile.SpritePositions.Big;
            EnableRaycastRange(enableSmall: true);
        }
        private void EnableRaycastRange(bool enableSmall = false, bool enableBig = false)
        {
            raycastRangesSmall.SetActive(enableSmall);
            raycastRangesBig.SetActive(enableBig);
        }
        #endregion

        #region On Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityBlock.left = hitInfo.IsBlock;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _proximityBlock.right = hitInfo.IsBlock;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => _proximityBlock.bottom = hitInfo.IsBlock;
        public void OnProximityRayHitTop(RayHitInfo hitInfo) => _proximityBlock.top = hitInfo.IsBlock;
        #endregion

        #region On Events
        public void OnFall()
        {
            if (!enabled)
                return;

            gameObject.SetActive(false);
            AllServices.PlayerService.Kill();
        }
        private void OnTimeOut()
        {
            if (!AllServices.GameDataService.IsGoalReached)
                Kill();
        }
        #endregion

        #region Other Methods
        public void BounceJump()
        {
            IsJumping = true;
            _lastJumpPressed = Time.time - _profile.Jump.MinBufferTime / 2f; // redusco a la mitad el buffer de salto minimo
            _currentSpeed.y = 0;
        }
        public void DamagePlayer()
        {
            if (IsInvensible)
                return;

            if (this.Mode == PlayerModes.Small)
                Kill();
            else
                Nerf();
        }
        public void Kill()
        {
            if (IsDead)
                return;

            IsDead = true;
            enabled = false;
            AllServices.PlayerService.Kill();
            _render.sortingLayerName = "Dead";
        }
        public void Buff()
        {
            _playerSoundFX.PlayBuff();

            if (this.Mode == PlayerModes.Super)
                return;

            AllServices.PlayerService.CanMove = false;
            AllServices.TimeService.StopTimer();

            this.Mode = this.Mode == PlayerModes.Small ? PlayerModes.Big : PlayerModes.Super;
            SetBigCollider();
        }
        public void Nerf()
        {
            if (this.Mode == PlayerModes.Small)
                return;

            IsInvensible = true;
            _playerSoundFX.PlayNerf();

            AllServices.PlayerService.CanMove = false;
            AllServices.TimeService.StopTimer();

            this.Mode = PlayerModes.Small;
        }
        public void RefreshCollider() => SetModeCollider(this.Mode);
        private void SetModeCollider(PlayerModes playerMode)
        {
            if (playerMode == PlayerModes.Small)
                SetSmallCollider();
            else
                SetBigCollider();
        }
        #endregion
    }
}