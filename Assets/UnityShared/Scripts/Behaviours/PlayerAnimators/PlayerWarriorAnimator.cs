using System.Collections;
using UnityEngine;

namespace UnityShared.Behaviours.PlayerAnimators
{
    public class PlayerWarriorAnimator : Base3DPlayerAnimator
    {
        private Coroutine attackCO;

        // animation IDs
        private int _animIDAttack;

        protected override void Start()
        {
            base.Start();
            _animIDAttack = Animator.StringToHash("Attack");
        }

        public void SetAttack()
        {
            attackCO ??= StartCoroutine(SetAttackCO());
        }

        private IEnumerator SetAttackCO()
        {
            _animator.SetLayerWeight(_animator.GetLayerIndex("Attack Layer"), 1);
            _animator.SetTrigger("Attack");

            yield return new WaitForSeconds(0.9f);
            _animator.SetLayerWeight(_animator.GetLayerIndex("Attack Layer"), 0);

            attackCO = null;
        }
    }
}