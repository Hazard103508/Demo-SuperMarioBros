using Mario.Application.Services;
using Mario.Game.Boxes;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System;
using System.Collections;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Items
{
    public class Mushroom : MonoBehaviour, ITopHitable, IBottomHitable, ILeftHitable, IRightHitable
    {
        [SerializeField] private MushroomProfile _mushroomProfile;

        private Vector3 _currentSpeed;
        private Bounds<bool> _proximityBlock = new();
        private bool _isRising;
        private bool _isJumping;
        private GameObject _hitBox;

        #region Unity Methods
        protected virtual void Awake()
        {
            _isRising = true;
            _currentSpeed = Vector2.right * _mushroomProfile.MoveSpeed;
        }
        private void Start()
        {
            StartCoroutine(RiseMushroom());
        }
        private void Update()
        {
            if (_isRising)
                return;

            if (!AllServices.PlayerService.CanMove)
                return;

            CalculateWalk();
            CalculateGravity();

            Move();
        }
        #endregion

        #region Private Methods
        private void CalculateWalk()
        {
            if (_proximityBlock.right)
                _currentSpeed.x = -Mathf.Abs(_currentSpeed.x);
            else if (_proximityBlock.left)
                _currentSpeed.x = Mathf.Abs(_currentSpeed.x);
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

            _isRising = false;
        }
        private void Jump()
        {
            if (!_isJumping)
            {
                _isJumping = true;
                _currentSpeed.y = _mushroomProfile.JumpAcceleration * Time.deltaTime;
            }
        }
        #endregion

        #region External Events
        public void OnFall() => Destroy(gameObject);
        #endregion

        #region On Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityBlock.left = hitInfo.IsBlock;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _proximityBlock.right = hitInfo.IsBlock;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo)
        {
            _proximityBlock.bottom = hitInfo.IsBlock;

            foreach (var obj in hitInfo.hitObjects)
            {
                if (_hitBox != null && obj.Equals(_hitBox))
                    continue;

                var box = obj.GetComponent<BottomHitableBox>();
                if (box != null && box.IsJumping)
                {
                    _hitBox = obj;
                    Jump();

                    if (Math.Sign(_currentSpeed.x) != Math.Sign(this.transform.position.x - obj.transform.position.x))
                        _currentSpeed.x *= -1;

                    return;
                }
            };

            _hitBox = null;
            _isJumping = false;
        }
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