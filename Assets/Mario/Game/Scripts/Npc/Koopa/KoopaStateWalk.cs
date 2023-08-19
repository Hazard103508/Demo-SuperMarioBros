using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateWalk : KoopaState
    {
        #region Objects
        private readonly Koopa _koopa;
        #endregion

        #region Constructor
        public KoopaStateWalk(Koopa koopa)
        {
            _koopa = koopa;
        }
        #endregion

        #region Private Methods
        private void KillKoopa(Vector3 hitPosition)
        {
            _koopa.StateMachine.TransitionTo(_koopa.StateMachine.StateDead);
            _koopa.ChangeSpeedAfferHit(hitPosition);
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _koopa.Movable.enabled = true;
            _koopa.Movable.Speed = _koopa.Profile.MoveSpeed;
            _koopa.Animator.SetTrigger("Idle");
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                _koopa.ChangeDirectionToRight();
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                _koopa.ChangeDirectionToLeft();
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player)
        {
            _koopa.StateMachine.TransitionTo(_koopa.StateMachine.StateInShell);
            player.BounceJump();
        }
        public override void OnHittedByPlayerFromLeft(PlayerController player) => player.DamagePlayer();
        public override void OnHittedByPlayerFromRight(PlayerController player) => player.DamagePlayer();
        public override void OnHittedByPlayerFromBottom(PlayerController player) => player.DamagePlayer();
        #endregion

        #region On Box Hit
        public override void OnHittedByBox(GameObject box) => KillKoopa(box.transform.position);
        #endregion

        #region On Koopa Hit
        public override void OnHittedByKoppa(Koopa koopa) => KillKoopa(koopa.transform.position);
        #endregion

        #region On Fireball Hit
        public override void OnHittedByFireBall(Fireball fireball) => KillKoopa(fireball.transform.position);
        #endregion
    }
}