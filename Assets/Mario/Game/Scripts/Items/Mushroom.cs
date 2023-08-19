using Mario.Game.Commons;
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
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByBox
    {
        #region Objects
        [SerializeField] private MushroomProfile _mushroomProfile;
        private Movable _movable;
        private bool _isJumping;
        #endregion

        #region Properties
        protected bool IsRising { get; private set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _movable = GetComponent<Movable>();
            _movable.Speed = _mushroomProfile.MoveSpeed;
            _movable.Gravity = _mushroomProfile.FallSpeed;
            _movable.MaxFallSpeed = _mushroomProfile.MaxFallSpeed;
        }
        private void OnEnable()
        {
            ResetMushroom();
            StartCoroutine(RiseMushroom());
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

            _movable.enabled = true;
            IsRising = false;
        }
        private void ChangeDirectionToRight() => _movable.Speed = Mathf.Abs(_movable.Speed);
        private void ChangeDirectionToLeft() => _movable.Speed = -Mathf.Abs(_movable.Speed);
        #endregion

        #region On local Ray Range Hit
        public void OnBottomCollided(RayHitInfo hitInfo) => _isJumping = false;
        public void OnLeftCollided(RayHitInfo hitInfo) => ChangeDirectionToRight();
        public void OnRightCollided(RayHitInfo hitInfo) => ChangeDirectionToLeft();
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

            _isJumping = true;
            _movable.AddJumpForce(_mushroomProfile.JumpAcceleration);

            if (Math.Sign(_movable.Speed) != Math.Sign(this.transform.position.x - box.transform.position.x))
                _movable.Speed *= -1;
        }
        #endregion
    }
}