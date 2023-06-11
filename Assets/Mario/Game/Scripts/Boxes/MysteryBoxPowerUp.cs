using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class MysteryBoxPowerUp : BottomHitableBox
    {
        private GameObject _itemPrefab;

        [SerializeField] private AudioSource _risingSoundFX;
        [SerializeField] protected MysteryBoxPowerUpProfile _powerUpBoxProfile;
        [SerializeField] private Animator _spriteAnimator;

        protected override void Awake()
        {
            base.Awake();
            AllServices.SceneService.AddAsset(_powerUpBoxProfile.RedMushroomReference);
            AllServices.SceneService.AddAsset(_powerUpBoxProfile.FlowerReference);
        }
        public override void OnHitFromBottom(PlayerController player)
        {
            if (!IsHitable)
                return;

            PlayHitSoundFX();

            base.OnHitFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");
            _risingSoundFX.Play();

            _itemPrefab = player.Mode == Enums.PlayerModes.Small ?
                AllServices.SceneService.GetAssetReference<GameObject>(_powerUpBoxProfile.RedMushroomReference) :
                AllServices.SceneService.GetAssetReference<GameObject>(_powerUpBoxProfile.FlowerReference);
        }
        public override void OnJumpCompleted()
        {
            base.OnJumpCompleted();
            base.InstantiateContent(_itemPrefab);
        }
    }
}