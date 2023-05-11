using Mario.Game.Handlers;
using Mario.Game.Player;
using Mario.Game.Rewards;
using Mario.Game.ScriptableObjects;
using UnityEngine;

namespace Mario.Game.Props
{
    public class MysteryBox : TopHitableBlock
    {
        [SerializeField] private Animator _spriteAnimator;

        public override void HitTop(PlayerController player)
        {
            if (!IsHitable)
                return;

            base.HitTop(player);
            _spriteAnimator.SetTrigger("Disable");

            base.InstantiateReward();
        }
    }
}