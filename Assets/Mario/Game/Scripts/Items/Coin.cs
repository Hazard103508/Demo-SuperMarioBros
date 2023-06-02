using Mario.Application.Services;
using Mario.Game.Enums;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Items
{
    public class Coin : MonoBehaviour, ITopHitable, IBottomHitable, ILeftHitable, IRightHitable
    {
        [SerializeField] protected CoinProfile _profile;
        private bool isCollected;

        private void Awake()
        {
            AllServices.SceneService.AddAsset(_profile.CoinSoundFXReference);
        }

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

            PlayCollectSound();
            Destroy(gameObject);
        }
        private void PlayCollectSound()
        {
            var soundFX = AllServices.SceneService.GetAssetReference<GameObject>(_profile.CoinSoundFXReference);
            var soundFXObj = Instantiate(soundFX);
            soundFXObj.transform.position = this.transform.position;
        }
    }
}