using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class MysteryBoxCoin : Box.Box
    {
        #region Objects
        [SerializeField] protected MysteryBoxCoinProfile _mysteryBoxProfile;
        [SerializeField] private Animator _spriteAnimator;
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController_OLD player)
        {
            if (!IsHitable)
                return;

            //PlayHitSoundFX();

            base.OnHittedByPlayerFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");

            base.ShowContent(_mysteryBoxProfile.CoinPoolReference);
        }
        #endregion
    }
}