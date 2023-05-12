using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class MysteryBox : BottomHitableBlock
    {
        [SerializeField] private Animator _spriteAnimator;
        [SerializeField] private bool _instantiateItemOnHit;

        public override void OnHitFromBottom(PlayerController player)
        {
            if (!IsHitable)
                return;

            base.OnHitFromBottom(player);
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