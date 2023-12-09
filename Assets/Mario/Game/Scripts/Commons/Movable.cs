using Mario.Game.Interfaces;
using System.Linq;
using UnityEngine;

namespace Mario.Game.Commons
{
    public class Movable : MonoBehaviour
    {
        #region Objects
        public bool ChekCollisions;
        public RaycastRange RaycastTop;
        public RaycastRange RaycastBottom;
        public RaycastRange RaycastLeft;
        public RaycastRange RaycastRight;

        private Vector2 _nextPosition;
        private Vector2 _currentSpeed;
        private IHittableByMovingToTop hittableByMovingToTop;
        private IHittableByMovingToBottom hittableByMovingToBottom;
        private IHittableByMovingToLeft hittableByMovingToLeft;
        private IHittableByMovingToRight hittableByMovingToRight;
        #endregion

        #region Properties
        public float Gravity { get; set; }
        public float MaxFallSpeed { get; set; }
        public float Speed
        {
            get => _currentSpeed.x;
            set
            {
                _currentSpeed.x = value;
            }
        }
        public float JumpForce => _currentSpeed.y;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _currentSpeed = Vector2.zero;
            hittableByMovingToTop = GetComponent<IHittableByMovingToTop>();
            hittableByMovingToBottom = GetComponent<IHittableByMovingToBottom>();
            hittableByMovingToLeft = GetComponent<IHittableByMovingToLeft>();
            hittableByMovingToRight = GetComponent<IHittableByMovingToRight>();
        }
        private void FixedUpdate()
        {
            ApplyGravity();

            _nextPosition = (Vector2)transform.position + _currentSpeed * Time.deltaTime;
            if (ChekCollisions)
            {
                CalculateCollision_Top(ref _nextPosition);
                CalculateCollision_Bottom(ref _nextPosition);
                CalculateCollision_Right(ref _nextPosition);
                CalculateCollision_Left(ref _nextPosition);
            }
        }
        private void LateUpdate()
        {
            Move(_nextPosition);
        }
        private void OnEnable()
        {
            _nextPosition = transform.position;
        }
        #endregion

        #region Public
        public void SetJumpForce(float force) => _currentSpeed.y = force;
        public void RemoveGravity()
        {
            Gravity = 0;
            MaxFallSpeed = 0;
            _nextPosition = new Vector2(_nextPosition.x, transform.position.y);
        }
        public void SetNextYPosition(float yPosition)
        {
            _nextPosition = new Vector2(_nextPosition.x, yPosition);
            transform.position = _nextPosition;
        }
        #endregion

        #region Private Methods
        private void ApplyGravity()
        {
            _currentSpeed.y -= Gravity * Time.deltaTime;
            if (_currentSpeed.y < -MaxFallSpeed)
                _currentSpeed.y = -MaxFallSpeed;
        }
        private void CalculateCollision_Top(ref Vector2 nextPosition)
        {
            if (RaycastTop == null)
                return;

            float rayExtraLength = nextPosition.y - transform.position.y;
            if (rayExtraLength > 0)
            {
                var hitInfo = RaycastTop.CalculateCollision(rayExtraLength);
                if (hitInfo.IsBlock)
                {
                    var hitObject = hitInfo.hitObjects.First();
                    nextPosition.y = GetFixedPositionY(hitObject.Point, RaycastTop);
                }

                if (hittableByMovingToTop != null && hitInfo.hitObjects.Any())
                    hittableByMovingToTop.OnHittedByMovingToTop(hitInfo);
            }
        }
        private void CalculateCollision_Bottom(ref Vector2 nextPosition)
        {
            if (RaycastBottom == null)
                return;

            float rayExtraLength = transform.position.y - nextPosition.y;
            if (rayExtraLength > 0)
            {
                var hitInfo = RaycastBottom.CalculateCollision(rayExtraLength);
                if (hitInfo.IsBlock)
                {
                    var hitObject = hitInfo.hitObjects.First();
                    nextPosition.y = GetFixedPositionY(hitObject.Point, RaycastBottom);
                    _currentSpeed.y = 0;
                }

                if (hittableByMovingToBottom != null && hitInfo.hitObjects.Any())
                    hittableByMovingToBottom.OnHittedByMovingToBottom(hitInfo);
            }
        }
        private void CalculateCollision_Right(ref Vector2 nextPosition)
        {
            if (RaycastRight == null)
                return;

            float rayExtraLength = nextPosition.x - transform.position.x;
            if (rayExtraLength >= 0)
            {
                var hitInfo = RaycastRight.CalculateCollision(rayExtraLength);
                if (hitInfo.IsBlock)
                {
                    var hitObject = hitInfo.hitObjects.First();
                    nextPosition.x = GetFixedPositionX(hitObject.Point, RaycastRight);
                }

                if (hittableByMovingToRight != null && hitInfo.hitObjects.Any())
                    hittableByMovingToRight.OnHittedByMovingToRight(hitInfo);
            }
        }
        private void CalculateCollision_Left(ref Vector2 nextPosition)
        {
            if (RaycastLeft == null)
                return;

            float rayExtraLength = transform.position.x - nextPosition.x;
            if (rayExtraLength >= 0)
            {
                var hitInfo = RaycastLeft.CalculateCollision(rayExtraLength);
                if (hitInfo.IsBlock)
                {
                    var hitObject = hitInfo.hitObjects.First();
                    nextPosition.x = GetFixedPositionX(hitObject.Point, RaycastLeft);
                }

                if (hittableByMovingToLeft != null && hitInfo.hitObjects.Any())
                    hittableByMovingToLeft.OnHittedByMovingToLeft(hitInfo);
            }
        }
        private float GetFixedPositionX(Vector2 hitPoint, RaycastRange raycast)
        {
            var rayLocalPos = transform.position.x - raycast.transform.position.x;
            return hitPoint.x + rayLocalPos - raycast.Profile.Range.EndPoint.x - (raycast.Profile.Ray.Length * raycast.Profile.Ray.Direction.x);
        }
        private float GetFixedPositionY(Vector2 hitPoint, RaycastRange raycast)
        {
            var rayLocalPos = transform.position.y - raycast.transform.position.y;
            return hitPoint.y + rayLocalPos - raycast.Profile.Range.EndPoint.y - (raycast.Profile.Ray.Length * raycast.Profile.Ray.Direction.y);
        }
        private void Move(Vector2 nextPosition)
        {
            transform.position = nextPosition;
        }
        #endregion
    }
}