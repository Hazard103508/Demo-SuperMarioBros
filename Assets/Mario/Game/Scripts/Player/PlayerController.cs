using Mario.Game.Enums;
using Mario.Game.Interfaces;
using Mario.Game.Props;
using Mario.Game.ScriptableObjects;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private PlayerProfile _profile;
        private ControllerVariables _controllerVariables;
        #endregion

        #region Properties
        public PlayerInput Input { get; private set; }
        public Vector3 RawMovement { get; private set; }
        public float WalkSpeedFactor => Mathf.Abs(_controllerVariables.currentSpeed.x) / _profile.Walk.MaxSpeed;
        public bool IsGrounded => _controllerVariables.ProximityHit.bottom;
        private bool JumpMinBuffered => _controllerVariables.lastJumpPressed + _profile.Jump.MinBufferTime > Time.time;
        private bool JumpMaxBuffered
        {
            get
            {
                float absCurrentSpeed = Mathf.Abs(_controllerVariables.currentSpeed.x);
                if (absCurrentSpeed > _profile.Walk.MaxSpeed)
                {
                    float maxSpeedDif = _profile.Run.MaxSpeed - _profile.Walk.MaxSpeed;
                    float runSpeedDif = absCurrentSpeed - _profile.Walk.MaxSpeed;
                    float runSpeedFactor = runSpeedDif / maxSpeedDif;
                    return _controllerVariables.lastJumpPressed + (_profile.Jump.MaxRunBufferTime * runSpeedFactor) > Time.time;
                }
                else
                    return _controllerVariables.lastJumpPressed + _profile.Jump.MaxWalkBufferTime > Time.time;
            }
        }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _controllerVariables = new ControllerVariables();
            Input = new PlayerInput();
            SetSpriteSize();
        }
        private void Update()
        {
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
            var _jumpDown = Input.JumpDown;
            Input = new PlayerInput
            {
                JumpDown = UnityEngine.Input.GetKey(KeyCode.X),
                X = UnityEngine.Input.GetAxisRaw("Horizontal"),
                Run = UnityEngine.Input.GetKey(KeyCode.Z),
            };

            if (IsGrounded && !_jumpDown && Input.JumpDown)
                _controllerVariables.lastJumpPressed = Time.time;
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

            if (_controllerVariables.currentSpeed.x > 0 && _controllerVariables.ProximityHit.right || _controllerVariables.currentSpeed.x < 0 && _controllerVariables.ProximityHit.left)
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
            if (JumpMinBuffered || (Input.JumpDown && JumpMaxBuffered))
                _controllerVariables.currentSpeed.y += _profile.Jump.Acceleration * Time.deltaTime;

            if (_controllerVariables.currentSpeed.y > _profile.Jump.MaxSpeed)
                _controllerVariables.currentSpeed.y = _profile.Jump.MaxSpeed;

            if (_controllerVariables.ProximityHit.top)
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
            if (RawMovement.y == 0)
            {
                float fixPos = (int)nextPosition.y + 0.5f;
                float dif = fixPos - nextPosition.y;
                nextPosition.y = nextPosition.y + dif; // ajusto diferencia en posicion del personaje
            }

            // fuerzo ajuste de posicion en los lados de los bloques 
            if (!_controllerVariables.ProximityHit.bottom)
            {
              if (!_controllerVariables.ProximityHit.left && _controllerVariables.ProximityHit.right)
                  nextPosition.x -= _profile.Jump.HorizontalAdjustmentSpeed * Time.deltaTime;
              if (!_controllerVariables.ProximityHit.right && _controllerVariables.ProximityHit.left)
                  nextPosition.x += _profile.Jump.HorizontalAdjustmentSpeed * Time.deltaTime;
            }

            transform.position = nextPosition;
        }
        #endregion

        #region On Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _controllerVariables.ProximityHit.left = hitInfo.IsHiting;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _controllerVariables.ProximityHit.right = hitInfo.IsHiting;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => _controllerVariables.ProximityHit.bottom = hitInfo.IsHiting;
        public void OnProximityRayHitTop(RayHitInfo hitInfo) => _controllerVariables.ProximityHit.top = hitInfo.IsHiting;
        #endregion

        #region Other Methods
        private void SetSpriteSize()
        {
            var render = GetComponent<SpriteRenderer>();
            _controllerVariables.spriteSize = render.sprite.bounds.size;
        }
        #endregion

        #region Classes
        internal class ControllerVariables
        {
            public Bounds<bool> ProximityHit = new Bounds<bool>();
            public Vector2 currentSpeed;
            public Vector2 spriteSize;
            public float lastJumpPressed;
        }
        #endregion
    }
}