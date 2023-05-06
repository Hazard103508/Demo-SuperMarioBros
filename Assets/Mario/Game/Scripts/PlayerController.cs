using Mario.Game.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerProfile playerProfile;

        private ControllerVariables _controllerVariables;


        public PlayerInput Input { get; private set; }
        public Vector3 RawMovement { get; private set; }
        public float SpeedFactor => Mathf.Abs(_controllerVariables.currentSpeed.x) / playerProfile.Walk.MaxSpeed;
        public bool Grounded => _controllerVariables.collitionBounds.bottom;


        private void Awake()
        {
            _controllerVariables = new ControllerVariables();
            Input = new PlayerInput();
            SetSpriteSize();
        }
        private void Update()
        {

            GatherInput();
            RunCollisionChecks();

            CalculateWalk();    // Horizontal movement
            CalculateGravity(); // Vertical movement
            CalculateJump();

            MoveCharacter();    // Actually perform the axis movement
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
        private void GatherInput()
        {
            var _jumpDown = Input.JumpDown;
            Input = new PlayerInput
            {
                JumpDown = UnityEngine.Input.GetKey(KeyCode.X),
                X = UnityEngine.Input.GetAxisRaw("Horizontal"),
                Run = UnityEngine.Input.GetKey(KeyCode.Z),
            };

            if (Grounded && !_jumpDown && Input.JumpDown)
                _lastJumpPressed = Time.time;
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


        // We use these raycast checks for pre-collision information
        private void RunCollisionChecks()
        {
            var rayBounds = CalculateRayRanged(); // Generate ray ranges. 

            _controllerVariables.collitionBounds.bottom = RunCollitionDetection(rayBounds.bottom);
            _controllerVariables.collitionBounds.top = RunCollitionDetection(rayBounds.top);
            _controllerVariables.collitionBounds.left = RunCollitionDetection(rayBounds.left);
            _controllerVariables.collitionBounds.right = RunCollitionDetection(rayBounds.right);
        }
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
        private bool RunCollitionDetection(RayRange range) => EvaluateRayPositions(range).Any(point => Physics2D.Raycast(point, range.Dir, _controllerVariables.detectionRayLength, playerProfile.GroundLayer));
        private void SetSpriteSize()
        {
            var render = GetComponent<SpriteRenderer>();
            _controllerVariables.spriteSize = render.sprite.bounds.size;
        }
        private void CalculateGravity()
        {
            _controllerVariables.currentSpeed.y -= playerProfile.Fall.FallSpeed * Time.deltaTime;
            if (Grounded)
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

        public float MaxJumpSpeed = 12;
        public float jumpSpeed = 300;
        public float jumpMinBuffer = 0.05f;
        public float jumpMaxBuffer = 0.25f;
        public float jumpMaxRunBuffer = 0.35f;
        private float _lastJumpPressed;

        private bool MinBufferedJump => _lastJumpPressed + jumpMinBuffer > Time.time;
        private bool MaxBufferedJump
        {
            get
            {
                float absCurrentSpeed = Mathf.Abs(_controllerVariables.currentSpeed.x);
                if (absCurrentSpeed > playerProfile.Walk.MaxSpeed)
                {
                    float maxSpeedDif = playerProfile.Run.MaxSpeed - playerProfile.Walk.MaxSpeed;
                    float runSpeedDif = absCurrentSpeed - playerProfile.Walk.MaxSpeed;
                    float runSpeedFactor = runSpeedDif / maxSpeedDif;
                    return _lastJumpPressed + (jumpMaxRunBuffer * runSpeedFactor) > Time.time;
                }
                else
                    return _lastJumpPressed + jumpMaxBuffer > Time.time;
            }
        }

        private void CalculateJump()
        {
            if (MinBufferedJump || (Input.JumpDown && MaxBufferedJump))
                _controllerVariables.currentSpeed.y += jumpSpeed * Time.deltaTime;

            if (_controllerVariables.currentSpeed.y > MaxJumpSpeed)
                _controllerVariables.currentSpeed.y = MaxJumpSpeed;

            if (_controllerVariables.collitionBounds.top)
            {
                if (_controllerVariables.currentSpeed.y > 0)
                    _controllerVariables.currentSpeed.y = 0;
            }
        }
    }
    internal class ControllerVariables
    {
        public int detectorCount = 3;
        public float detectionRayLength = 0.1f;
        public float rayBuffer = 0.15f;
        public float bottomRayBuffer = 0.05f;

        public Bounds<bool> collitionBounds = new Bounds<bool>();
        public Vector2 currentSpeed;
        public Vector2 spriteSize;
    }
    public class PlayerInput
    {
        public float X { get; set; }
        public bool JumpDown { get; set; }
        //public bool JumpUp { get; set; }
        public bool Run { get; set; }
    }
    public struct RayRange
    {
        public RayRange(float x1, float y1, float x2, float y2, Vector2 dir)
        {
            Start = new Vector2(x1, y1);
            End = new Vector2(x2, y2);
            Dir = dir;
        }

        public readonly Vector2 Start, End, Dir;
    }
}