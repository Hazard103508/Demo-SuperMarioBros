using Mario.Application.Services;
using Mario.Game.Enums;
using Mario.Game.ScriptableObjects.Map;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityShared.Behaviours.Various.RaycastRange;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private SpriteRenderer _render;
        [SerializeField] private RaycastRange[] raycastRanges = null;

        private PlayerProfile _profile;
        private ControllerVariables _controllerVariables;
        private PlayerModes _mode;
        private bool _isDucking;
        #endregion

        #region Properties
        public PlayerInput Input { get; private set; }
        public Vector3 RawMovement { get; private set; }
        public PlayerModes Mode
        {
            get => _mode;
            set
            {
                if (_mode == value)
                    return;

                AllServices.CharacterService.StopMovement();
                AllServices.TimeService.StopTimer();

                _mode = value;

                if (value == PlayerModes.Small)
                    SetSmallCollider();
                else
                    SetBigCollider();
            }
        }

        public float WalkSpeedFactor => Mathf.Abs(_controllerVariables.currentSpeed.x) / _profile.Walk.MaxSpeed;
        public bool IsGrounded => _controllerVariables.ProximityBlock.bottom;
        private bool JumpMinBuffered => _controllerVariables.lastJumpPressed + _profile.Jump.MinBufferTime > Time.time;
        private bool JumpMaxBuffered
        {
            get
            {
                float absCurrentSpeed = Mathf.Abs(_controllerVariables.currentSpeed.x);
                if (absCurrentSpeed > _profile.Walk.MaxSpeed)
                {
                    float speedFactor = Mathf.InverseLerp(_profile.Walk.MaxSpeed, _profile.Run.MaxSpeed, absCurrentSpeed);
                    float finalBufferTime = Mathf.Lerp(_profile.Jump.MaxWalkBufferTime, _profile.Jump.MaxRunBufferTime, speedFactor);
                    return _controllerVariables.lastJumpPressed + finalBufferTime > Time.time;
                }
                else
                    return _controllerVariables.lastJumpPressed + _profile.Jump.MaxWalkBufferTime > Time.time;
            }
        }
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
                {
                    if (Mode == PlayerModes.Small)
                        SetSmallCollider();
                    else
                        SetBigCollider();
                }
            }
        }
        public bool IsStuck { get; set; }
        public bool IsDead { get; private set; }
        public bool IsAutoWalk { get; set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _profile = AllServices.GameDataService.PlayerProfile;
            _controllerVariables = new ControllerVariables();
            Input = new PlayerInput();
            transform.position = AllServices.GameDataService.CurrentMapProfile.StartPosition;

            AllServices.TimeService.OnTimeOut.AddListener(OnTimeOut);
            AllServices.GameDataService.OnMapCompleted.AddListener(OnMapCompleted);

            SetInitMode(PlayerModes.Small);
        }
        private void OnDestroy()
        {
            AllServices.TimeService.OnTimeOut.RemoveListener(OnTimeOut);
            AllServices.GameDataService.OnMapCompleted.RemoveListener(OnMapCompleted);
        }
        private void Update()
        {
            if (!AllServices.CharacterService.CanMove)
                return;

            GatherInput();

            CalculateWalk();
            CalculateGravity();
            CalculateJump();
        }
        private void LateUpdate()
        {
            if (!AllServices.CharacterService.CanMove)
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

            if (IsGrounded && !_jumpDown && Input.JumpDown)
            {
                IsJumping = true;
                _controllerVariables.lastJumpPressed = Time.time;
            }
        }
        private void CalculateWalk()
        {
            this.IsDucking = Mode != PlayerModes.Small && Input.X == 0 && Input.IsDucking;

            if (Input.X != 0 && !Input.IsDucking)
            {
                float currentAcceleration = Input.Run ? _profile.Run.Acceleration : _profile.Walk.Acceleration;
                _controllerVariables.currentSpeed.x += Input.X * currentAcceleration * Time.deltaTime;

                float currentSpeed = Input.Run ? _profile.Run.MaxSpeed : _profile.Walk.MaxSpeed;
                _controllerVariables.currentSpeed.x = Mathf.Clamp(_controllerVariables.currentSpeed.x, -currentSpeed, currentSpeed);
            }

            if (RawMovement.x != 0 && (Input.X == 0 || Mathf.Sign(RawMovement.x) != Mathf.Sign(Input.X) || Input.IsDucking))
            {
                float currentDeacceleration = Input.Run ? _profile.Run.Deacceleration : _profile.Walk.Deacceleration;
                _controllerVariables.currentSpeed.x = Mathf.MoveTowards(_controllerVariables.currentSpeed.x, 0, currentDeacceleration * Time.deltaTime);
            }

            if (_controllerVariables.currentSpeed.x > 0 && _controllerVariables.ProximityBlock.right || _controllerVariables.currentSpeed.x < 0 && _controllerVariables.ProximityBlock.left)
                _controllerVariables.currentSpeed.x = 0; // Don't walk through walls
        }
        private void CalculateGravity()
        {
            _controllerVariables.currentSpeed.y -= _profile.Fall.FallSpeed * Time.deltaTime;
            if (IsGrounded)
            {
                if (_controllerVariables.currentSpeed.y < 0)
                    _controllerVariables.currentSpeed.y = 0;
            }
            else
            {
                if (_controllerVariables.currentSpeed.y < -_profile.Fall.MaxFallSpeed)
                    _controllerVariables.currentSpeed.y = -_profile.Fall.MaxFallSpeed;
            }
        }
        private void CalculateJump()
        {
            if (IsAutoWalk)
            {
                IsJumping = false;
                if (_controllerVariables.currentSpeed.y > 0)
                    _controllerVariables.currentSpeed.y = 0;

                return;
            }

            if (IsJumping) // evita retomar la aceleracion del salto despues que este empezo a caer
            {
                if (JumpMinBuffered || (Input.JumpDown && JumpMaxBuffered))
                    _controllerVariables.currentSpeed.y += _profile.Jump.Acceleration * Time.deltaTime;
                else
                    IsJumping = false;
            }

            if (_controllerVariables.currentSpeed.y > _profile.Jump.MaxSpeed)
                _controllerVariables.currentSpeed.y = _profile.Jump.MaxSpeed;

            if (_controllerVariables.ProximityBlock.top)
            {
                if (_controllerVariables.currentSpeed.y > 0)
                {
                    _controllerVariables.currentSpeed.y = 0;
                    _controllerVariables.lastJumpPressed = 0;
                }
            }
        }
        private void MoveCharacter()
        {
            RawMovement = _controllerVariables.currentSpeed;
            var nextPosition = transform.position + RawMovement * Time.deltaTime;

            // ajusto posicion de contacto con el suelo
            if (IsGrounded && RawMovement.y == 0)
                nextPosition.y = Mathf.Round(nextPosition.y);

            // fuerzo ajuste de posicion en los lados de los bloques 
            if (!IsGrounded || IsStuck)
                nextPosition = AdjustHorizontalPosition(nextPosition);

            transform.position = nextPosition;
            IsStuck = false;

            AllServices.CharacterService.UpdatePlayerPositon(transform.position);
        }
        private Vector3 AdjustHorizontalPosition(Vector3 position)
        {
            if (!_controllerVariables.ProximityBlock.left && _controllerVariables.ProximityBlock.right)
                position.x -= _profile.Jump.HorizontalAdjustmentSpeed * Time.deltaTime;
            else if (!_controllerVariables.ProximityBlock.right && _controllerVariables.ProximityBlock.left)
                position.x += _profile.Jump.HorizontalAdjustmentSpeed * Time.deltaTime;
            else if (_controllerVariables.ProximityBlock.right && _controllerVariables.ProximityBlock.left)
                position.x += _profile.Jump.HorizontalAdjustmentSpeed * Time.deltaTime;

            return position;
        }
        private void SetSmallCollider()
        {
            _render.transform.localPosition = _profile.SmallPlayer.SpritePosition;
            raycastRanges[0].transform.parent.localPosition = _profile.SmallPlayer.Collider.position;
            Array.ForEach(raycastRanges, r => r.SpriteSize = new Size2(_profile.SmallPlayer.Collider.width, _profile.SmallPlayer.Collider.height));
        }
        private void SetBigCollider()
        {
            _render.transform.localPosition = _profile.BigPlayer.SpritePosition;
            raycastRanges[0].transform.parent.localPosition = _profile.BigPlayer.Collider.position;
            Array.ForEach(raycastRanges, r => r.SpriteSize = new Size2(_profile.BigPlayer.Collider.width, _profile.BigPlayer.Collider.height));
        }
        private void SetDuckingCollider()
        {
            _render.transform.localPosition = _profile.DuckingPlayer.SpritePosition;
            raycastRanges[0].transform.parent.localPosition = _profile.DuckingPlayer.Collider.position;
            Array.ForEach(raycastRanges, r => r.SpriteSize = new Size2(_profile.DuckingPlayer.Collider.width, _profile.DuckingPlayer.Collider.height));
        }
        #endregion

        #region On Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _controllerVariables.ProximityBlock.left = hitInfo.IsBlock;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _controllerVariables.ProximityBlock.right = hitInfo.IsBlock;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => _controllerVariables.ProximityBlock.bottom = hitInfo.IsBlock;
        public void OnProximityRayHitTop(RayHitInfo hitInfo) => _controllerVariables.ProximityBlock.top = hitInfo.IsBlock;
        #endregion

        #region On Events
        public void OnPlayerGrowUp() => this.Mode = PlayerModes.Big;
        public void OnPlayerSuper() => this.Mode = PlayerModes.Super;
        public void OnFall()
        {
            if (!enabled)
                return;

            gameObject.SetActive(false);
            AllServices.LifeService.Remove();
        }
        private void OnTimeOut()
        {
            if (!AllServices.GameDataService.IsMapCompleted)
                Kill();
        }
        private void OnMapCompleted() => gameObject.SetActive(false);
        #endregion

        #region Other Methods
        public void Kill()
        {
            IsDead = true;
            enabled = false;
            Mode = PlayerModes.Small;
            AllServices.LifeService.Remove();
        }
        private void SetInitMode(PlayerModes playerMode)
        {
            _mode = playerMode;
            SetModeCollider(_mode);
        }
        private void SetModeCollider(PlayerModes playerMode)
        {
            if (playerMode == PlayerModes.Small)
                SetSmallCollider();
            else
                SetBigCollider();
        }
        #endregion

        #region Classes
        internal class ControllerVariables
        {
            public Bounds<bool> ProximityBlock = new Bounds<bool>();
            public Vector2 currentSpeed;
            public float lastJumpPressed = 0;
        }
        #endregion
    }
}