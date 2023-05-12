using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects;
using System;
using System.Collections;
using UnityEngine;
using UnityShared.Behaviours.Various.RaycastRange;
using UnityShared.Commons.Structs;

namespace Mario.Game.Items
{
    public class Mushroom : MonoBehaviour, ITopHitable, IBottomHitable, ILeftHitable, IRightHitable
    {
        [SerializeField] protected MushroomProfile _profile;
        [SerializeField] private RaycastRange[] raycastRanges = null;

        private Vector3 _currentSpeed;
        private Bounds<bool> _proximityHit = new Bounds<bool>();
        private bool isRising;

        private void Awake()
        {
            isRising = true;
            _currentSpeed = Vector2.right * _profile.MoveSpeed;
            Array.ForEach(raycastRanges, r => r.SpriteSize = new Size2(1, 1));
        }
        private void Start()
        {
            StartCoroutine(RiseMushroom());
        }
        private void Update()
        {
            if (isRising)
                return;

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
                nextPosition.y = Mathf.Round(nextPosition.y);

            transform.position = nextPosition;
        }
        private IEnumerator RiseMushroom()
        {
            var _initPosition = transform.transform.position;
            var _targetPosition = _initPosition + Vector3.up;
            float _timer = 0;
            float _maxTime = 0.8f;
            while (_timer < _maxTime)
            {
                _timer += Time.deltaTime;
                var t = Mathf.InverseLerp(0, _maxTime, _timer);
                transform.localPosition = Vector3.Lerp(_initPosition, _targetPosition, t);
                yield return null;
            }

            isRising = false;
        }

        #region On Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityHit.left = hitInfo.IsHiting;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _proximityHit.right = hitInfo.IsHiting;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => _proximityHit.bottom = hitInfo.IsHiting;
        #endregion

        #region On Player Hit
        public virtual void HitTop(PlayerController player)
        {
        }
        public virtual void HitBottom(PlayerController player)
        {
        }
        public virtual void HitLeft(PlayerController player)
        {
        }
        public virtual void HitRight(PlayerController player)
        {
        }
        #endregion
    }
}