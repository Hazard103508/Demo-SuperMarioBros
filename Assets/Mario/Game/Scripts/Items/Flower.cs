using Mario.Application.Components;
using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Items
{
    public class Flower : ObjectPool, 
        IHitableByPlayerFromTop, 
        IHitableByPlayerFromBottom, 
        IHitableByPlayerFromLeft, 
        IHitableByPlayerFromRight
    {
        #region Objects
        [SerializeField] protected FlowerProfile _profile;
        private bool isCollected;
        private bool _isRising;
        #endregion

        #region Unity Methods
        protected override void OnEnable()
        {
            base.OnEnable();
            StartCoroutine(RiseFlower());
        }
        #endregion

        #region Private Methods
        private IEnumerator RiseFlower()
        {
            yield return new WaitForEndOfFrame();

            isCollected = false;
            _isRising = true;
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
        private void CollectFlower(PlayerController player)
        {
            if (isCollected || _isRising)
                return;

            isCollected = true;
            Services.ScoreService.Add(_profile.Points);
            Services.ScoreService.ShowPoint(_profile.Points, transform.position + Vector3.up * 1.25f, 0.8f, 3f);

            player.Buff();
            gameObject.SetActive(false);
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) => CollectFlower(player);
        public void OnHittedByPlayerFromBottom(PlayerController player) => CollectFlower(player);
        public void OnHittedByPlayerFromLeft(PlayerController player) => CollectFlower(player);
        public void OnHittedByPlayerFromRight(PlayerController player) => CollectFlower(player);
        #endregion
    }
}