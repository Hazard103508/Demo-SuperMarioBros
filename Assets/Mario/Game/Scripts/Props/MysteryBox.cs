using Mario.Game.Player;
using Mario.Game.ScriptableObjects;
using UnityEngine;

namespace Mario.Game.Props
{
    public class MysteryBox : TopHitableBlock
    {
        [SerializeField] private MisteryBoxProfile profile;
        [SerializeField] private Animator _spriteAnimator;

        public override void HitTop(PlayerController player)
        {
            if (!IsHitable)
                return;

            base.HitTop(player);
            _spriteAnimator.SetTrigger("Disable");

            var obj = Instantiate(profile.prefab);
            obj.transform.position = this.transform.position;
        }
        public override void OnJumpCompleted()
        {
        }
    }
}