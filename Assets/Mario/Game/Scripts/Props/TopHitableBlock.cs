using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Props
{
    public class TopHitableBlock : MonoBehaviour, ITopHitable
    {
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
        public virtual void OnJumpCompleted() => IsHitable = true;
    }
}