using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class Coin : MonoBehaviour, IHitableByPlayerFromTop, IHitableByPlayerFromBottom, IHitableByPlayerFromLeft, IHitableByPlayerFromRight, IHitableByBoxFromBottom
    {
        #region Objects
        [SerializeField] protected CoinProfile _profile;
        private bool isCollected;
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

        #region On Player Hit
        public void OnHitableByPlayerFromTop(PlayerController player) => CollectCoin();
        public void OnHitableByPlayerFromBottom(PlayerController player) => CollectCoin();
        public void OnHitableByPlayerFromLeft(PlayerController player) => CollectCoin();
        public void OnHitableByPlayerFromRight(PlayerController player) => CollectCoin();
        #endregion

        #region On Box Hit
        public void OnIHitableByBoxFromBottom(GameObject box) => CollectCoin();
        #endregion
    }
}