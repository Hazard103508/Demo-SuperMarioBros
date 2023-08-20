using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Items
{
    public class Flower : MonoBehaviour,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight
    {
        #region Objects
        [SerializeField] protected FlowerProfile _profile;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            StartCoroutine(RiseFlower());
        }
        #endregion

        #region Private Methods
        private IEnumerator RiseFlower()
        {
            yield return new WaitForEndOfFrame();

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
        }
        private void CollectFlower(PlayerController_OLD player)
        {
            gameObject.layer = 0;
            Services.ScoreService.Add(_profile.Points);
            Services.ScoreService.ShowPoints(_profile.Points, transform.position + Vector3.up * 1.75f, 0.8f, 3f);

            player.Buff();
            gameObject.SetActive(false);
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController_OLD player) => CollectFlower(player);
        public void OnHittedByPlayerFromBottom(PlayerController_OLD player) => CollectFlower(player);
        public void OnHittedByPlayerFromLeft(PlayerController_OLD player) => CollectFlower(player);
        public void OnHittedByPlayerFromRight(PlayerController_OLD player) => CollectFlower(player);
        #endregion
    }
}