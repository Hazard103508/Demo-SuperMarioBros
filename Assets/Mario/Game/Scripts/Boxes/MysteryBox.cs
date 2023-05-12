using Mario.Game.Handlers;
using Mario.Game.Player;
using Mario.Game.Items;
using Mario.Game.ScriptableObjects;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class MysteryBox : TopHitableBlock
    {
        [SerializeField] private Animator _spriteAnimator;
        [SerializeField] private bool _instantiateItemOnHit;

        public override void HitTop(PlayerController player)
        {
            if (!IsHitable)
                return;

            base.HitTop(player);
            _spriteAnimator.SetTrigger("Disable");

            if (_instantiateItemOnHit)
                base.InstantiateContent();
        }
        public override void OnJumpCompleted()
        {
            if (!_instantiateItemOnHit)
                base.InstantiateContent();
        }
    }
}