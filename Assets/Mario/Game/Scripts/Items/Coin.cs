using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class Coin : MonoBehaviour, ITopHitable, IBottomHitable, ILeftHitable, IRightHitable
    {
        [SerializeField] protected CoinProfile _profile;
        private bool isCollected;

        #region On Player Hit
        public void OnHitFromTop(PlayerController player) => CollectCoin(player);
        public void OnHitFromBottom(PlayerController player) => CollectCoin(player);
        public void OnHitFromLeft(PlayerController player) => CollectCoin(player);
        public void OnHitFromRight(PlayerController player) => CollectCoin(player);
        #endregion

        private void CollectCoin(PlayerController player)
        {
            if (isCollected)
                return;

            isCollected = true;
            AllServices.ScoreService.Add(_profile.Points);
            AllServices.CoinService.Add();
            Destroy(gameObject);
        }
    }
}