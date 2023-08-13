using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityShared.Behaviours.Various.RaycastRange;
using UnityShared.Commons.Structs;

namespace Mario.Game.Commons
{
    public class Movable : MonoBehaviour
    {
        #region Objects
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private CollitionInfo _bottomCollision;
        [SerializeField] private CollitionInfo _leftCollision;
        [SerializeField] private CollitionInfo _rightCollision;
        private Vector2 _currentSpeed;
        private Vector2 _spriteSize = Vector2.zero;
        #endregion

        #region Properties
        public float Gravity { get; set; }
        public float Speed
        {
            get => _currentSpeed.x;
            set
            {
                _currentSpeed.x = value;
            }
        }
        public float MaxFallSpeed { get; set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _currentSpeed = Vector2.zero;

            var scale = transform.localScale;
            _spriteSize = new Vector2(_renderer.sprite.bounds.size.x * scale.x, _renderer.sprite.bounds.size.y * scale.y) / 2.0f;
        }
        private void Update()
        {
            ApplyGravity();

            var nextPosition = (Vector2)transform.position + _currentSpeed * Time.deltaTime;
            CalculateCollision_Bottom(ref nextPosition);
            CalculateCollision_Right(ref nextPosition);
            CalculateCollision_Left(ref nextPosition);

            Move(nextPosition);
        }
        private void OnEnable()
        {
            _currentSpeed.y = 0;
        }
        #endregion

        #region Public
        public void AddJumpForce(float force) => _currentSpeed.y = force;
        #endregion

        #region Private Methods
        private void ApplyGravity()
        {
            _currentSpeed.y -= Gravity * Time.deltaTime;
            if (_currentSpeed.y < -MaxFallSpeed)
                _currentSpeed.y = -MaxFallSpeed;
        }
        private void CalculateCollision_Bottom(ref Vector2 nextPosition)
        {
            float rayLength = transform.position.y - nextPosition.y;
            if (rayLength > 0)
            {
                var hitInfo = _bottomCollision.RayCast.CalculateCollision(rayLength);
                if (hitInfo.IsBlock && _bottomCollision.FixPositionOnCollide)
                {
                    var hitObject = hitInfo.hitObjects.First();
                    nextPosition.y = hitObject.Point.y + _spriteSize.y;
                }

                if (hitInfo.hitObjects.Any())
                    _bottomCollision.Collided.Invoke(hitInfo);
            }
        }
        private void CalculateCollision_Right(ref Vector2 nextPosition)
        {
            float rayLength = nextPosition.x - transform.position.x;
            if (rayLength > 0)
            {
                var hitInfo = _rightCollision.RayCast.CalculateCollision(rayLength);
                if (hitInfo.IsBlock && _rightCollision.FixPositionOnCollide)
                {
                    var hitObject = hitInfo.hitObjects.First();
                    nextPosition.x = hitObject.Point.x - _spriteSize.x;
                }

                if (hitInfo.hitObjects.Any())
                    _rightCollision.Collided.Invoke(hitInfo);
            }
        }
        private void CalculateCollision_Left(ref Vector2 nextPosition)
        {
            float rayLength = transform.position.x - nextPosition.x;
            if (rayLength > 0)
            {
                var hitInfo = _leftCollision.RayCast.CalculateCollision(rayLength);
                if (hitInfo.IsBlock && _leftCollision.FixPositionOnCollide)
                {
                    var hitObject = hitInfo.hitObjects.First();
                    nextPosition.x = hitObject.Point.x + _spriteSize.x;
                }

                if (hitInfo.hitObjects.Any())
                    _leftCollision.Collided.Invoke(hitInfo);
            }
        }
        private void Move(Vector2 nextPosition)
        {
            transform.position = nextPosition;
        }
        #endregion

        #region Structures
        [Serializable]
        public class CollitionInfo
        {
            public bool FixPositionOnCollide;
            public RaycastRange RayCast;
            public UnityEvent<RayHitInfo> Collided;
        }
        #endregion
    }
}