using Mario.Game.Handlers;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.Rewards;
using UnityEngine;

namespace Mario.Game.Props
{
    public class TopHitableBlock : MonoBehaviour, ITopHitable
    {
        [SerializeField] private Reward _rewardPrefab;
        private Animator _boxAnimator;
        protected bool IsHitable { get; set; }

        private void Awake()
        {
            _boxAnimator = GetComponent<Animator>();
            IsHitable = true;
        }
        public virtual void HitTop(PlayerController player)
        {
            if (!IsHitable)
                return;

            _boxAnimator.SetTrigger("Jump");
            IsHitable = false;
        }
        public virtual void OnJumpCompleted()
        {
        }

        protected void InstantiateReward()
        {
            var reward = Instantiate(_rewardPrefab);
            reward.transform.position = this.transform.position;
        }
    }
}