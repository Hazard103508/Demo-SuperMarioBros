using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class Coin : MonoBehaviour, ITopHitable, IBottomHitable, ILeftHitable, IRightHitable, IBottomHitableByBox
    {
        [SerializeField] protected CoinProfile _profile;
        private bool isCollected;

        #region On Player Hit
        public void OnHitFromTop(PlayerController player) => CollectCoin();
        public void OnHitFromBottom(PlayerController player) => CollectCoin();
        public void OnHitFromLeft(PlayerController player) => CollectCoin();
        public void OnHitFromRight(PlayerController player) => CollectCoin();
        #endregion

        #region On Box Hit
        public void OnHitFromBottomByBox(GameObject box) => CollectCoin();
        #endregion

        #region Private Methods
        private void CollectCoin()
        {
            if (isCollected)
                return;

            isCollected = true;
            AllServices.ScoreService.Add(_profile.Points);
            AllServices.CoinService.Add();
            Destroy(gameObject);
        }
        #endregion
    }
}