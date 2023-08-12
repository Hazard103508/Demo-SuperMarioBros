using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System;
using System.Collections;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Items
{
    public class Mushroom : MonoBehaviour,
        IHitableByPlayerFromTop,
        IHitableByPlayerFromBottom,
        IHitableByPlayerFromLeft,
        IHitableByPlayerFromRight,
        IHitableByBox
    {
        #region Objects
        [SerializeField] private MushroomProfile _mushroomProfile;

        private Vector3 _currentSpeed;
        private readonly Bounds<bool> _proximityBlock = new();
        private bool _isJumping;
        #endregion

        #region Properties
        protected bool IsRising { get; private set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _currentSpeed = Vector2.right * _mushroomProfile.MoveSpeed;
        }
        private void OnEnable()
        {
            ResetMushroom();
            StartCoroutine(RiseMushroom());
        }
        private void Update()
        {
            if (IsRising)
                return;

            if (!Services.PlayerService.CanMove)
                return;

            CalculateWalk();
            CalculateGravity();

            Move();
        }
        #endregion

        #region Public Methods
        public void OnFall() => gameObject.SetActive(false);

        #endregion

        #region Protected Methods
        protected virtual void CollectMushroom(PlayerController player)
        {
        }
        protected virtual void ResetMushroom() => IsRising = false;
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
            yield return new WaitForEndOfFrame();

            IsRising = true;
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

            IsRising = false;
        }
        private void Jump()
        {
            if (!_isJumping)
            {
                _isJumping = true;
                _currentSpeed.y = _mushroomProfile.JumpAcceleration;
            }
        }
        #endregion

        #region On local Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityBlock.left = hitInfo.IsBlock;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _proximityBlock.right = hitInfo.IsBlock;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo)
        {
            _proximityBlock.bottom = hitInfo.IsBlock;
            _isJumping = !hitInfo.IsBlock;
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromLeft(PlayerController player) => CollectMushroom(player);
        public void OnHittedByPlayerFromBottom(PlayerController player) => CollectMushroom(player);
        public void OnHittedByPlayerFromRight(PlayerController player) => CollectMushroom(player);
        public void OnHittedByPlayerFromTop(PlayerController player) => CollectMushroom(player);
        #endregion

        #region On Box Hit
        public void OnHittedByBox(GameObject box)
        {
            if (_isJumping)
                return;

            Jump();

            if (Math.Sign(_currentSpeed.x) != Math.Sign(this.transform.position.x - box.transform.position.x))
                _currentSpeed.x *= -1;
        }
        #endregion
    }
}