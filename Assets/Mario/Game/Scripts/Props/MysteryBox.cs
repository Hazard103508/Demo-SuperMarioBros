using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Props
{
    public class MysteryBox : TopHitableBlock
    {
        [SerializeField] private Animator _spriteAnimator;

        public override void HitTop(PlayerController player)
        {
            base.HitTop(player);
            _spriteAnimator.SetTrigger("Disable");
        }
        public override void OnJumpCompleted()
        {
        }
    }
}