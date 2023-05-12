using UnityEngine;
using Mario.Game.ScriptableObjects;
using UnityShared.Commons.Structs;
using UnityShared.Behaviours.Various.RaycastRange;
using System;

namespace Mario.Game.Items
{
    public class Mushroom : MonoBehaviour
    {
        [SerializeField] private MushroomProfile _profile;
        [SerializeField] private RaycastRange[] raycastRanges = null;

        private Vector3 _currentSpeed;
        private Bounds<bool> _proximityHit = new Bounds<bool>();
        private float adjustmentPositionY = 0.5f;

        private void Awake()
        {
            _currentSpeed = Vector2.right * _profile.MoveSpeed;
            Array.ForEach(raycastRanges, r => r.SpriteSize = new Size2(1, 1));
        }
        private void Update()
        {
            CalculateWalk();
            CalculateGravity();
            Move();
        }

        private void CalculateWalk()
        {
            if (_currentSpeed.x > 0 && _proximityHit.right || _proximityHit.left)
                _currentSpeed.x *= -1;
        }
        private void CalculateGravity()
        {
            _currentSpeed.y -= _profile.FallSpeed * Time.deltaTime;
            if (_proximityHit.bottom)
            {
                if (_currentSpeed.y < 0)
                    _currentSpeed.y = 0;
            }
            else
            {
                if (_currentSpeed.y < -_profile.MaxFallSpeed)
                    _currentSpeed.y = -_profile.MaxFallSpeed;
            }
        }
        private void Move()
        {
            var nextPosition = transform.position + _currentSpeed * Time.deltaTime;

            // ajusto posicion de contacto con el suelo
            if (_proximityHit.bottom && _currentSpeed.y == 0)
            {
                float fixPos = (int)nextPosition.y + adjustmentPositionY;
                float dif = fixPos - nextPosition.y;
                nextPosition.y = nextPosition.y + dif;
            }

            transform.position = nextPosition;
        }

        #region On Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityHit.left = hitInfo.IsHiting;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _proximityHit.right = hitInfo.IsHiting;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => _proximityHit.bottom = hitInfo.IsHiting;
        #endregion
    }
}