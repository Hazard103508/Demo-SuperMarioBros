using Mario.Game.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private PlayerProfile playerProfile;
        private ControllerVariables _controllerVariables;
        #endregion

        #region Properties
        public PlayerInput Input { get; private set; }
        public Vector3 RawMovement { get; private set; }
        public float WalkSpeedFactor => Mathf.Abs(_controllerVariables.currentSpeed.x) / playerProfile.Walk.MaxSpeed;
        public bool IsGrounded => _controllerVariables.collitionBounds.bottom;
        private bool JumpMinBuffered => _controllerVariables.lastJumpPressed + playerProfile.Jump.MinBufferTime > Time.time;
        private bool JumpMaxBuffered
        {
            get
            {
                float absCurrentSpeed = Mathf.Abs(_controllerVariables.currentSpeed.x);
                if (absCurrentSpeed > playerProfile.Walk.MaxSpeed)
                {
                    float maxSpeedDif = playerProfile.Run.MaxSpeed - playerProfile.Walk.MaxSpeed;
                    float runSpeedDif = absCurrentSpeed - playerProfile.Walk.MaxSpeed;
                    float runSpeedFactor = runSpeedDif / maxSpeedDif;
                    return _controllerVariables.lastJumpPressed + (playerProfile.Jump.MaxRunBufferTime * runSpeedFactor) > Time.time;
                }
                else
                    return _controllerVariables.lastJumpPressed + playerProfile.Jump.MaxWalkBufferTime > Time.time;
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

            CalculateCollision();
            CalculateWalk();
            CalculateGravity();
            CalculateJump();

            MoveCharacter();    // Actually perform the axis movement
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
        private void CalculateCollision()
        {
            var rayBounds = CalculateRayRanged(); // Generate ray ranges. 

            _controllerVariables.collitionBounds.bottom = CalculateCollisionDetection(rayBounds.bottom);
            _controllerVariables.collitionBounds.top = CalculateCollisionDetection(rayBounds.top);
            _controllerVariables.collitionBounds.left = CalculateCollisionDetection(rayBounds.left);
            _controllerVariables.collitionBounds.right = CalculateCollisionDetection(rayBounds.right);
        }
        private void CalculateWalk()
        {
            if (Input.X != 0)
            {
                float currentAcceleration = Input.Run ? playerProfile.Run.Acceleration : playerProfile.Walk.Acceleration;
                _controllerVariables.currentSpeed.x += Input.X * currentAcceleration * Time.deltaTime;

                float currentSpeed = Input.Run ? playerProfile.Run.MaxSpeed : playerProfile.Walk.MaxSpeed;
                _controllerVariables.currentSpeed.x = Mathf.Clamp(_controllerVariables.currentSpeed.x, -currentSpeed, currentSpeed);
            }

            if (RawMovement.x != 0 && (Input.X == 0 || Mathf.Sign(RawMovement.x) != Mathf.Sign(Input.X)))
            {
                float currentDeacceleration = Input.Run ? playerProfile.Run.Deacceleration : playerProfile.Walk.Deacceleration;
                _controllerVariables.currentSpeed.x = Mathf.MoveTowards(_controllerVariables.currentSpeed.x, 0, currentDeacceleration * Time.deltaTime);
            }


            if (_controllerVariables.currentSpeed.x > 0 && _controllerVariables.collitionBounds.right || _controllerVariables.currentSpeed.x < 0 && _controllerVariables.collitionBounds.left)
                _controllerVariables.currentSpeed.x = 0; // Don't walk through walls
        }
        private void CalculateGravity()
        {
            _controllerVariables.currentSpeed.y -= playerProfile.Fall.FallSpeed * Time.deltaTime;
            if (IsGrounded)
            {
                if (_controllerVariables.currentSpeed.y < 0)
                    _controllerVariables.currentSpeed.y = 0;
            }
            else
            {
                if (_controllerVariables.currentSpeed.y < -playerProfile.Fall.MaxFallSpeed)
                    _controllerVariables.currentSpeed.y = -playerProfile.Fall.MaxFallSpeed;
            }
        }
        private void CalculateJump()
        {
            if (JumpMinBuffered || (Input.JumpDown && JumpMaxBuffered))
                _controllerVariables.currentSpeed.y += playerProfile.Jump.Acceleration * Time.deltaTime;

            if (_controllerVariables.currentSpeed.y > playerProfile.Jump.MaxSpeed)
                _controllerVariables.currentSpeed.y = playerProfile.Jump.MaxSpeed;

            if (_controllerVariables.collitionBounds.top)
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

            transform.position = nextPosition;
        }
        #endregion

        #region Other Methods
        private bool CalculateCollisionDetection(RayRange range) => EvaluateRayPositions(range).Any(point => Physics2D.Raycast(point, range.Dir, _controllerVariables.detectionRayLength, playerProfile.GroundLayer));
        private IEnumerable<Vector2> EvaluateRayPositions(RayRange range)
        {
            for (var i = 0; i < _controllerVariables.detectorCount; i++)
            {
                var t = (float)i / (_controllerVariables.detectorCount - 1);
                yield return Vector2.Lerp(range.Start, range.End, t);
            }
        }
        private Bounds<RayRange> CalculateRayRanged()
        {
            var b = new Bounds(transform.position, _controllerVariables.spriteSize);
            return new Bounds<RayRange>()
            {
                bottom = new RayRange(b.min.x + _controllerVariables.bottomRayBuffer, b.min.y, b.max.x - _controllerVariables.bottomRayBuffer, b.min.y, Vector2.down),
                top = new RayRange(b.min.x + _controllerVariables.rayBuffer, b.max.y, b.max.x - _controllerVariables.rayBuffer, b.max.y, Vector2.up),
                left = new RayRange(b.min.x, b.min.y + _controllerVariables.rayBuffer, b.min.x, b.max.y - _controllerVariables.rayBuffer, Vector2.left),
                right = new RayRange(b.max.x, b.min.y + _controllerVariables.rayBuffer, b.max.x, b.max.y - _controllerVariables.rayBuffer, Vector2.right),
            };
        }
        private void SetSpriteSize()
        {
            var render = GetComponent<SpriteRenderer>();
            _controllerVariables.spriteSize = render.sprite.bounds.size;
        }
        #endregion

        #region Classes
        internal class ControllerVariables
        {
            public int detectorCount = 3;
            public float detectionRayLength = 0.1f;
            public float rayBuffer = 0.15f;
            public float bottomRayBuffer = 0.05f;

            public Bounds<bool> collitionBounds = new Bounds<bool>();
            public Vector2 currentSpeed;
            public Vector2 spriteSize;
            public float lastJumpPressed;
        }
        #endregion
    }
}