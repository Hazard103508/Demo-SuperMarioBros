using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class MysteryBoxPowerUp : Box
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
        }
        #endregion

        #region Protected Methods
        protected override void OnJumpCompleted()
        {
            base.OnJumpCompleted();
            base.InstantiateContent(_powerUpBoxProfile.RedMushroomPoolReference);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            if (!IsHitable)
                return;

            PlayHitSoundFX();

            base.OnHittedByPlayerFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");
            _risingSoundFX.Play();

            //_itemPrefab = player.Mode == Enums.PlayerModes.Small ?
            //    AllServices.AddressablesService.GetAssetReference(_powerUpBoxProfile.RedMushroomPoolReference) :
            //    AllServices.AddressablesService.GetAssetReference(_powerUpBoxProfile.FlowerPoolReference);
        }
        #endregion
    }
}