using Mario.Application.Services;
using Mario.Game.Enums;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Items
{
    public class Flower : MonoBehaviour, ITopHitable, IBottomHitable, ILeftHitable, IRightHitable
    {
        [SerializeField] protected FlowerProfile _profile;
        private bool isCollected;

        private void Start()
        {
            StartCoroutine(RiseFlower());
        }
        private IEnumerator RiseFlower()
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
        }

        #region On Player Hit
        public void OnHitFromTop(PlayerController player) => CollectFlower(player);
        public void OnHitFromBottom(PlayerController player) => CollectFlower(player);
        public void OnHitFromLeft(PlayerController player) => CollectFlower(player);
        public void OnHitFromRight(PlayerController player) => CollectFlower(player);
        #endregion

        private void CollectFlower(PlayerController player)
        {
            if (isCollected)
                return;

            isCollected = true;
            AllServices.ScoreService.Add(_profile.Points);
            AllServices.ScoreService.ShowPoint(_profile.Points, transform.position + Vector3.up * 1.25f, 0.8f, 3f);
            player.Mode = player.Mode == PlayerModes.Small ? PlayerModes.Big : PlayerModes.Super;
            Destroy(gameObject);
        }
    }
}