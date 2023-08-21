using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class MysteryBoxPowerUp : Box.Box
    {
        #region Objects
        [SerializeField] private AudioSource _risingSoundFX;
        [SerializeField] protected MysteryBoxPowerUpProfile _powerUpBoxProfile;
        [SerializeField] private Animator _spriteAnimator;
        private PooledObjectProfile _buffReference;
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
            base.ShowContent(_buffReference);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController_OLD player)
        {
            if (!IsHitable)
                return;

            PlayHitSoundFX();

            base.OnHittedByPlayerFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");
            _risingSoundFX.Play();

            _buffReference = player.Mode == Enums.PlayerModes.Small ? _powerUpBoxProfile.RedMushroomPoolReference : _powerUpBoxProfile.FlowerPoolReference;
        }
        #endregion
    }
}