using Mario.Application.Services;
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
        public bool ChekCollisions;
        public CollitionInfo Bottom;
        public CollitionInfo Left;
        public CollitionInfo Right;

        private Vector2 _currentSpeed;
        private Vector2 _spriteSize = Vector2.zero;
        private Vector2 _pivot = Vector2.zero;
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
            _spriteSize = new Vector2(_renderer.sprite.bounds.size.x * scale.x, _renderer.sprite.bounds.size.y * scale.y);
            _pivot = new Vector2(_renderer.sprite.pivot.x / _renderer.sprite.rect.width, _renderer.sprite.pivot.y / _renderer.sprite.rect.height);
        }
        private void Update()
        {
            if (!Services.PlayerService.CanMove)
                return;

            ApplyGravity();

            var nextPosition = (Vector2)transform.position + _currentSpeed * Time.deltaTime;
            if (ChekCollisions)
            {
                CalculateCollision_Bottom(ref nextPosition);
                CalculateCollision_Right(ref nextPosition);
                CalculateCollision_Left(ref nextPosition);
            }

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
            float rayExtraLength = transform.position.y - nextPosition.y;
            if (rayExtraLength > 0)
            {
                var hitInfo = Bottom.RayCast.CalculateCollision(rayExtraLength);
                if (hitInfo.IsBlock && Bottom.FixPositionOnCollide)
                {
                    var hitObject = hitInfo.hitObjects.First();
                    nextPosition.y = hitObject.Point.y + _spriteSize.y * _pivot.y - _renderer.transform.localPosition.y;
                }

                if (hitInfo.hitObjects.Any())
                    Bottom.Collided.Invoke(hitInfo);
            }
        }
        private void CalculateCollision_Right(ref Vector2 nextPosition)
        {
            float rayExtraLength = nextPosition.x - transform.position.x;
            if (rayExtraLength > 0)
            {
                var hitInfo = Right.RayCast.CalculateCollision(rayExtraLength);
                if (hitInfo.IsBlock && Right.FixPositionOnCollide)
                {
                    var hitObject = hitInfo.hitObjects.First();
                    nextPosition.x = hitObject.Point.x - _spriteSize.x * _pivot.x - _renderer.transform.localPosition.x;
                }

                if (hitInfo.hitObjects.Any())
                    Right.Collided.Invoke(hitInfo);
            }
        }
        private void CalculateCollision_Left(ref Vector2 nextPosition)
        {
            float rayExtraLength = transform.position.x - nextPosition.x;
            if (rayExtraLength > 0)
            {
                var hitInfo = Left.RayCast.CalculateCollision(rayExtraLength);
                if (hitInfo.IsBlock && Left.FixPositionOnCollide)
                {
                    var hitObject = hitInfo.hitObjects.First();
                    nextPosition.x = hitObject.Point.x + _spriteSize.x * _pivot.x - _renderer.transform.localPosition.x;
                }

                if (hitInfo.hitObjects.Any())
                    Left.Collided.Invoke(hitInfo);
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