using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Npc;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class Coin : MonoBehaviour, 
        IHitableByPlayerFromTop, 
        IHitableByPlayerFromBottom, 
        IHitableByPlayerFromLeft, 
        IHitableByPlayerFromRight, 
        IHitableByBox,
        IHitableByKoppa
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
        private void CollectJumpingCoin()
        {
            CollectCoin();
            var jumpingCoin = AllServices.PoolService.GetObjectFromPool(_profile.CoinPoolReference);
            jumpingCoin.transform.position = this.transform.position + Vector3.down;
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) => CollectCoin();
        public void OnHittedByPlayerFromBottom(PlayerController player) => CollectCoin();
        public void OnHittedByPlayerFromLeft(PlayerController player) => CollectCoin();
        public void OnHittedByPlayerFromRight(PlayerController player) => CollectCoin();
        #endregion

        #region On Box Hit
        public void OnHittedByBox(GameObject box) => CollectJumpingCoin();
        #endregion

        #region On Koopa Hit
        public void OnHittedByKoppa(Koopa koopa) => CollectJumpingCoin();
        #endregion
    }
}