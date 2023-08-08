using Mario.Application.Services;
using Mario.Game.Enums;
using Mario.Game.ScriptableObjects.Map;
using Mario.Game.ScriptableObjects.Player;
using System.Linq;
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
        [SerializeField] private SquareRaycast raycastRangesBig;
        [SerializeField] private SquareRaycast raycastRangesSmall;

        private PlayerProfile _profile;
        private Bounds<RayHitInfo> _proximityBlock = new Bounds<RayHitInfo>();
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

                Services.PlayerService.CurrentMode = _mode = value;
            }
        }

        public float WalkSpeedFactor => Mathf.Abs(_currentSpeed.x) / _profile.Walk.MaxSpeed;
        public bool IsGrounded => _proximityBlock != null && _proximityBlock.bottom != null && _proximityBlock.bottom.IsBlock;
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
            Services.TimeService.OnTimeOut.AddListener(OnTimeOut);
            Services.PlayerService.CanMove = false;

            _profile = Services.GameDataService.PlayerProfile;
            Input = new PlayerInput();
            transform.position = Services.GameDataService.CurrentMapProfile.MapInit.StartPosition;

            _mode = Services.PlayerService.CurrentMode;
            SetModeCollider(_mode);
        }
        private void OnDestroy()
        {
            Services.TimeService.OnTimeOut.RemoveListener(OnTimeOut);
        }
        private void Update()
        {
            if (!Services.PlayerService.CanMove)
                return;

            GatherInput();

            CalculateWalk();
            CalculateGravity();
            CalculateJump();

        }
        private void LateUpdate()
        {
            if (!Services.PlayerService.CanMove)
                return;

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

            if (_currentSpeed.x > 0 && _proximityBlock.right.IsBlock || _currentSpeed.x < 0 && _proximityBlock.left.IsBlock)
                _currentSpeed.x = 0; // se detiene si hay obstaculo
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

            if (_proximityBlock != null && _proximityBlock.top != null && _proximityBlock.top.IsBlock && _currentSpeed.y > 0)
            {
                _currentSpeed.y = 0;
                _lastJumpPressed = 0;
            }
        }
        private void MoveCharacter()
        {
            RawMovement = _currentSpeed;
            var nextPosition = transform.position + RawMovement * Time.deltaTime;
            transform.position = nextPosition;

            var playerRaycast = raycastRangesBig.gameObject.activeSelf ? raycastRangesBig : raycastRangesSmall;
            playerRaycast.CalculateCollision(); // valido coliciones 

            SetHorizontalAlignment(ref nextPosition);
            SetVerticalAlignment(ref nextPosition);
            transform.position = nextPosition;
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
            raycastRangesSmall.gameObject.SetActive(enableSmall);
            raycastRangesBig.gameObject.SetActive(enableBig);
        }
        private void SetHorizontalAlignment(ref Vector3 nextPosition)
        {
            if (_proximityBlock.right.IsBlock && !_proximityBlock.left.IsBlock)
            {
                var hitObject = _proximityBlock.right.hitObjects.First();
                nextPosition.x = hitObject.Point.x - (0.5f + hitObject.RelativePosition.x);
            }
            else if (_proximityBlock.right.IsBlock || _proximityBlock.left.IsBlock)
            {
                var hitObject = _proximityBlock.left.hitObjects.First();
                nextPosition.x = hitObject.Point.x - (0.5f + hitObject.RelativePosition.x);
            }
        }
        private void SetVerticalAlignment(ref Vector3 nextPosition)
        {
            if (IsGrounded && RawMovement.y <= 0)
                nextPosition.y = Mathf.Round(nextPosition.y);
        }
        #endregion

        #region On Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityBlock.left = hitInfo;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _proximityBlock.right = hitInfo;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => _proximityBlock.bottom = hitInfo;
        public void OnProximityRayHitTop(RayHitInfo hitInfo) => _proximityBlock.top = hitInfo;
        #endregion

        #region On Events
        public void OnFall()
        {
            if (!enabled)
                return;

            gameObject.SetActive(false);
            Services.PlayerService.Kill();
        }
        private void OnTimeOut()
        {
            if (!Services.GameDataService.IsGoalReached)
                Kill();
        }
        #endregion

        #region Other Methods
        public void BounceJump()
        {
            IsJumping = true;
            _lastJumpPressed = Time.time - _profile.Jump.MaxRunBufferTime;
            _currentSpeed.y = _profile.Jump.BounceSpeed;
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
            Services.PlayerService.Kill();
            _render.sortingLayerName = "Dead";
        }
        public void Buff()
        {
            _playerSoundFX.PlayBuff();

            if (this.Mode == PlayerModes.Super)
                return;

            Services.PlayerService.CanMove = false;
            Services.TimeService.StopTimer();

            this.Mode = this.Mode == PlayerModes.Small ? PlayerModes.Big : PlayerModes.Super;
            SetBigCollider();
        }
        public void Nerf()
        {
            if (this.Mode == PlayerModes.Small)
                return;

            IsInvensible = true;
            _playerSoundFX.PlayNerf();

            Services.PlayerService.CanMove = false;
            Services.TimeService.StopTimer();

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