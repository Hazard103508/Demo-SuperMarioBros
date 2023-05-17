using Mario.Game.Player;
using Mario.Game.ScriptableObjects;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class MysteryBox : BottomHitableBlock
    {
        [SerializeField] protected MysteryBoxProfile _mysteryBoxProfile;
        [SerializeField] private Animator _spriteAnimator;

        public override void OnHitFromBottom(PlayerController player)
        {
            if (!IsHitable)
                return;

            base.OnHitFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");

            if (_mysteryBoxProfile.InstantiateItemOnHit)
                base.InstantiateContent(_mysteryBoxProfile.Item);
        }
        public override void OnJumpCompleted()
        {
            if (!_mysteryBoxProfile.InstantiateItemOnHit)
                base.InstantiateContent(_mysteryBoxProfile.Item);
        }
    }
}