using Mario.Game.Enums;
using Mario.Game.Handlers;
using Mario.Game.ScriptableObjects;
using System;
using UnityEngine;
using UnityShared.Behaviours.Various.RaycastRange;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private PlayerProfile _profile;
        [SerializeField] private RaycastRange[] raycastRanges = null;
        private ControllerVariables _controllerVariables;
        private PlayerModes _mode;
        #endregion

        #region Properties
        public PlayerInput Input { get; private set; }
        public Vector3 RawMovement { get; private set; }
        public PlayerModes Mode
        {
            get => _mode;
            set
            {
                if (_mode == PlayerModes.Small)
                    transform.position += Vector3.up * _controllerVariables.smallAdjustmentPositionY;

                if (_mode != value)
                    GameHandler.Instance.FreezeMove();

                _mode = value;
                var size = value == PlayerModes.Small ? _profile.sizes.small : _profile.sizes.big;
                Array.ForEach(raycastRanges, r => r.SpriteSize = size);
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
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _controllerVariables = new ControllerVariables();
            Input = new PlayerInput();
            Mode = PlayerModes.Small;
        }
        private void Update()
        {
            if (!GameHandler.Instance.AllowMove)
                return;

            GatherInput();

            CalculateWalk();
            CalculateGravity();
            CalculateJump();
            MoveCharacter();

            if (UnityEngine.Input.GetKey(KeyCode.R))
                transform.position = new Vector3(transform.position.x, 4f);
        }
        #endregion

        #region Update Methods
        private void GatherInput()
        {
            var _jumpDown = Input.JumpDown;
            Input = new PlayerInput
            {
                JumpDown = UnityEngine.Input.GetKey(KeyCode.X),
                X = UnityEngine.Input.GetAxisRaw("Horizontal"),
                Run = UnityEngine.Input.GetKey(KeyCode.Z),
            };

            if (IsGrounded && !_jumpDown && Input.JumpDown)
            {
                IsJumping = true;
                _controllerVariables.lastJumpPressed = Time.time;
            }
        }
        private void CalculateWalk()
        {
            if (Input.X != 0)
            {
                float currentAcceleration = Input.Run ? _profile.Run.Acceleration : _profile.Walk.Acceleration;
                _controllerVariables.currentSpeed.x += Input.X * currentAcceleration * Time.deltaTime;

                float currentSpeed = Input.Run ? _profile.Run.MaxSpeed : _profile.Walk.MaxSpeed;
                _controllerVariables.currentSpeed.x = Mathf.Clamp(_controllerVariables.currentSpeed.x, -currentSpeed, currentSpeed);
            }

            if (RawMovement.x != 0 && (Input.X == 0 || Mathf.Sign(RawMovement.x) != Mathf.Sign(Input.X)))
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
            {
                if (this.Mode == PlayerModes.Small)
                {
                    float fixPos = (int)nextPosition.y + _controllerVariables.smallAdjustmentPositionY;
                    float dif = fixPos - nextPosition.y;
                    nextPosition.y = nextPosition.y + dif; // ajusto diferencia en posicion del personaje
                }
                else
                    nextPosition.y = Mathf.Round(nextPosition.y);
            }

            // fuerzo ajuste de posicion en los lados de los bloques 
            if (!IsGrounded)
            {
                if (!_controllerVariables.ProximityBlock.left && _controllerVariables.ProximityBlock.right)
                    nextPosition.x -= _profile.Jump.HorizontalAdjustmentSpeed * Time.deltaTime;
                if (!_controllerVariables.ProximityBlock.right && _controllerVariables.ProximityBlock.left)
                    nextPosition.x += _profile.Jump.HorizontalAdjustmentSpeed * Time.deltaTime;
            }

            transform.position = nextPosition;
        }
        #endregion

        #region On Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _controllerVariables.ProximityBlock.left = hitInfo.IsBlock;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _controllerVariables.ProximityBlock.right = hitInfo.IsBlock;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => _controllerVariables.ProximityBlock.bottom = hitInfo.IsBlock;
        public void OnProximityRayHitTop(RayHitInfo hitInfo) => _controllerVariables.ProximityBlock.top = hitInfo.IsBlock;
        #endregion

        #region EventListener
        public void OnPlayerGrowUp() => this.Mode = PlayerModes.Big;
        public void OnPlayerSuper() => this.Mode = PlayerModes.Super;
        #endregion

        #region Classes
        internal class ControllerVariables
        {
            public float smallAdjustmentPositionY = 0.5f;
            public Bounds<bool> ProximityBlock = new Bounds<bool>();
            public Vector2 currentSpeed;
            public float lastJumpPressed = 0;
        }
        #endregion
    }
}