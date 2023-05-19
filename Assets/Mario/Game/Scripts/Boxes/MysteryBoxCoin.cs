using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class MysteryBoxCoin : BottomHitableBox
    {
        [SerializeField] protected MysteryBoxCoinProfile _mysteryBoxProfile;
        [SerializeField] private Animator _spriteAnimator;

        public override void OnHitFromBottom(PlayerController player)
        {
            _hitSoundFX.Play();

            if (!IsHitable)
                return;

            base.OnHitFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");

            base.InstantiateContent(_mysteryBoxProfile.CoinPrefab);
        }
    }
}