using Mario.Application.Services;
using Mario.Game.Enums;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class RedMushroom : Mushroom
    {
        [SerializeField] private RedMushroomProfile _redMushroomProfile;
        private bool isCollected;

        protected override void Awake()
        {
            base.Awake();
            AllServices.SceneService.AddAsset(_redMushroomProfile.PowerUpFXReference);
        }

        public override void OnHitFromLeft(PlayerController player) => CollectMushroom(player);
        public override void OnHitFromBottom(PlayerController player) => CollectMushroom(player);
        public override void OnHitFromRight(PlayerController player) => CollectMushroom(player);
        public override void OnHitFromTop(PlayerController player) => CollectMushroom(player);

        private void CollectMushroom(PlayerController player)
        {
            if (isCollected)
                return;

            isCollected = true;
            AllServices.ScoreService.Add(_redMushroomProfile.Points);
            AllServices.ScoreService.ShowPoint(_redMushroomProfile.Points, transform.position + Vector3.up * 1.25f, 0.8f, 3f);

            if (player.Mode == PlayerModes.Small)
                player.Mode = PlayerModes.Big;

            PlayCollectSound();
            Destroy(gameObject);
        }
        private void PlayCollectSound()
        {
            var soundFX = AllServices.SceneService.GetAssetReference<GameObject>(_redMushroomProfile.PowerUpFXReference);
            var soundFXObj = Instantiate(soundFX);
            soundFXObj.transform.position = this.transform.position;
        }
    }
}