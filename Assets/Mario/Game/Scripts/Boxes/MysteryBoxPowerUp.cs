using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class MysteryBoxPowerUp : BottomHitableBox
    {
        #region Objects
        [SerializeField] private AudioSource _risingSoundFX;
        [SerializeField] protected MysteryBoxPowerUpProfile _powerUpBoxProfile;
        [SerializeField] private Animator _spriteAnimator;
        private GameObject _itemPrefab;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            AllServices.SceneService.AddAsset(_powerUpBoxProfile.RedMushroomReference);
            AllServices.SceneService.AddAsset(_powerUpBoxProfile.FlowerReference);
        }
        #endregion

        #region Protected Methods
        protected override void OnJumpCompleted()
        {
            base.OnJumpCompleted();
            base.InstantiateContent(_itemPrefab);
        }
        #endregion

        #region On Player Hit
        public override void OnHitableByPlayerFromBottom(PlayerController player)
        {
            if (!IsHitable)
                return;

            PlayHitSoundFX();

            base.OnHitableByPlayerFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");
            _risingSoundFX.Play();

            _itemPrefab = player.Mode == Enums.PlayerModes.Small ?
                AllServices.SceneService.GetAssetReference<GameObject>(_powerUpBoxProfile.RedMushroomReference) :
                AllServices.SceneService.GetAssetReference<GameObject>(_powerUpBoxProfile.FlowerReference);
        }
        #endregion
    }
}