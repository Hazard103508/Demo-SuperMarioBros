using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System;
using System.Collections;
using UnityEngine;
using UnityShared.Behaviours.Various.RaycastRange;
using UnityShared.Commons.Structs;

namespace Mario.Game.Items
{
    public class Mushroom : MonoBehaviour, ITopHitable, IBottomHitable, ILeftHitable, IRightHitable
    {
        [SerializeField] private MushroomProfile _mushroomProfile;
        [SerializeField] private RaycastRange[] raycastRanges = null;

        private Vector3 _currentSpeed;
        private Bounds<bool> _proximityBlock = new Bounds<bool>();
        private bool isRising;

        private void Awake()
        {
            isRising = true;
            _currentSpeed = Vector2.right * _mushroomProfile.MoveSpeed;
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

            if (!AllServices.CharacterService.CanMove)
                return;

            CalculateWalk();
            CalculateGravity();
            Move();
        }

        private void CalculateWalk()
        {
            if (_currentSpeed.x > 0 && _proximityBlock.right || _proximityBlock.left)
                _currentSpeed.x *= -1;
        }
        private void CalculateGravity()
        {
            _currentSpeed.y -= _mushroomProfile.FallSpeed * Time.deltaTime;
            if (_proximityBlock.bottom)
            {
                if (_currentSpeed.y < 0)
                    _currentSpeed.y = 0;
            }
            else
            {
                if (_currentSpeed.y < -_mushroomProfile.MaxFallSpeed)
                    _currentSpeed.y = -_mushroomProfile.MaxFallSpeed;
            }
        }
        private void Move()
        {
            var nextPosition = transform.position + _currentSpeed * Time.deltaTime;

            // ajusto posicion de contacto con el suelo
            if (_proximityBlock.bottom && _currentSpeed.y == 0)
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
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityBlock.left = hitInfo.IsBlock;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _proximityBlock.right = hitInfo.IsBlock;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => _proximityBlock.bottom = hitInfo.IsBlock;
        #endregion

        #region On Player Hit
        public virtual void OnHitFromTop(PlayerController player)
        {
        }
        public virtual void OnHitFromBottom(PlayerController player)
        {
        }
        public virtual void OnHitFromLeft(PlayerController player)
        {
        }
        public virtual void OnHitFromRight(PlayerController player)
        {
        }
        #endregion
    }
}