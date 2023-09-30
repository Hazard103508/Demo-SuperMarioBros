using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Npc.Koopa;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class Coin : MonoBehaviour,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByBox,
        IHittableByKoppa
    {
        #region Objects
        [SerializeField] protected CoinProfile _profile;
        private bool isCollected;
        #endregion

        #region Private Methods
        private void CollectCoin(bool addPoint)
        {
            if (isCollected)
                return;

            isCollected = true;
            if (addPoint)
            {
                Services.ScoreService.Add(_profile.Points);
                Services.CoinService.Add();
            }
            Destroy(gameObject);
        }
        private void CollectJumpingCoin()
        {
            CollectCoin(false);
            Services.PoolService.GetObjectFromPool(_profile.CoinPoolReference, this.transform.position + Vector3.down);
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) => CollectCoin(true);
        public void OnHittedByPlayerFromBottom(PlayerController player) => CollectCoin(true);
        public void OnHittedByPlayerFromLeft(PlayerController player) => CollectCoin(true);
        public void OnHittedByPlayerFromRight(PlayerController player) => CollectCoin(true);
        #endregion

        #region On Box Hit
        public void OnHittedByBox(GameObject box) => CollectJumpingCoin();
        #endregion

        #region On Koopa Hit
        public void OnHittedByKoppa(Koopa koopa) => CollectJumpingCoin();
        #endregion
    }
}