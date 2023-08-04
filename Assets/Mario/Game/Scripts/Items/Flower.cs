using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Items
{
    public class Flower : MonoBehaviour, IHitableByPlayerFromTop, IHitableByPlayerFromBottom, IHitableByPlayerFromLeft, IHitableByPlayerFromRight
    {
        #region Objects
        [SerializeField] protected FlowerProfile _profile;
        private bool isCollected;
        private bool _isRising;
        #endregion

        #region Unity Methods
        private void Start()
        {
            StartCoroutine(RiseFlower());
        }
        #endregion

        #region Private Methods
        private IEnumerator RiseFlower()
        {
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
            AllServices.ScoreService.Add(_profile.Points);
            AllServices.ScoreService.ShowPoint(_profile.Points, transform.position + Vector3.up * 1.25f, 0.8f, 3f);

            player.Buff();
            Destroy(gameObject);
        }
        #endregion

        #region On Player Hit
        public void OnHitableByPlayerFromTop(PlayerController player) => CollectFlower(player);
        public void OnHitableByPlayerFromBottom(PlayerController player) => CollectFlower(player);
        public void OnHitableByPlayerFromLeft(PlayerController player) => CollectFlower(player);
        public void OnHitableByPlayerFromRight(PlayerController player) => CollectFlower(player);
        #endregion
    }
}