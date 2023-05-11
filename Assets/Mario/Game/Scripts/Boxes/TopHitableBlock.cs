using Mario.Game.Handlers;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.Items;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class TopHitableBlock : MonoBehaviour, ITopHitable
    {
        [SerializeField] private GameObject _contentPrefab;
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
            var reward = Instantiate(_contentPrefab);
            reward.transform.position = this.transform.position;
        }
    }
}