using Mario.Game.Handlers;
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

            var obj = Instantiate(profile.Prefab);
            obj.transform.position = this.transform.position;

            GameDataHandler.Instance.IncreaseScore(profile.Score, transform.position);
        }
        public override void OnJumpCompleted()
        {
        }
    }
}