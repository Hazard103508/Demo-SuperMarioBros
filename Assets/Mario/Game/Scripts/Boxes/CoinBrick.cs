using Mario.Game.Handlers;
using Mario.Game.Player;
using Mario.Game.Rewards;
using Mario.Game.ScriptableObjects;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class CoinBrick : TopHitableBlock
    {
        [SerializeField] private Animator _spriteAnimator;
        [SerializeField] private float _limitTime;

        private bool _firstHit;
        private bool _isEmpty;

        public override void HitTop(PlayerController player)
        {
            if (!IsHitable)
                return;

            _firstHit = true;

            base.HitTop(player);

            if (_limitTime < 0)
            {
                _isEmpty = true;
                _spriteAnimator.SetTrigger("Disable");
            }

            base.InstantiateReward();
        }
        public override void OnJumpCompleted()
        {
            IsHitable = !_isEmpty;
        }
        private void Update()
        {
            if (_limitTime > 0 && _firstHit)
                _limitTime -= Time.deltaTime;
        }
    }
}