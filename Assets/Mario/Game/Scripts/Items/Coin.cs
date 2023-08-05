using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class Coin : MonoBehaviour, IHitableByPlayerFromTop, IHitableByPlayerFromBottom, IHitableByPlayerFromLeft, IHitableByPlayerFromRight, IHitableByBox
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
        public void OnHittedByPlayerFromTop(PlayerController player) => CollectCoin();
        public void OnHittedByPlayerFromBottom(PlayerController player) => CollectCoin();
        public void OnHittedByPlayerFromLeft(PlayerController player) => CollectCoin();
        public void OnHittedByPlayerFromRight(PlayerController player) => CollectCoin();
        #endregion

        #region On Box Hit
        public void OnHittedByBox(GameObject box)
        {
            CollectCoin();

            var jumpingCoin = AllServices.PoolService.GetObjectFromPool(_profile.CoinPoolReference);
            jumpingCoin.transform.position = box.transform.position;
        }
        #endregion
    }
}