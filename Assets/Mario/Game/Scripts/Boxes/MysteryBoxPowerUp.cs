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
            AllServices.AssetReferencesService.Add(_powerUpBoxProfile.RedMushroomReference);
            AllServices.AssetReferencesService.Add(_powerUpBoxProfile.FlowerReference);
        }
        public override void OnHitFromBottom(PlayerController player)
        {
            _hitSoundFX.Play();

            if (!IsHitable)
                return;

            base.OnHitFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");
            _risingSoundFX.Play();

            _itemPrefab = player.Mode == Enums.PlayerModes.Small ?
                AllServices.AssetReferencesService.GetObjectReference<GameObject>(_powerUpBoxProfile.RedMushroomReference) :
                AllServices.AssetReferencesService.GetObjectReference<GameObject>(_powerUpBoxProfile.FlowerReference);
        }
        public override void OnJumpCompleted()
        {
            base.InstantiateContent(_itemPrefab);
        }
    }
}