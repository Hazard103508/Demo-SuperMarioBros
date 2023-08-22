using Mario.Game.Interactable;
using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateWalk : KoopaState
    {
        #region Constructor
        public KoopaStateWalk(Koopa koopa) : base(koopa)
        {
        }
        #endregion

        #region Private Methods
        private void KillKoopa(Vector3 hitPosition)
        {
            Koopa.StateMachine.TransitionTo(Koopa.StateMachine.StateDead);
            Koopa.ChangeSpeedAfferHit(hitPosition);
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Koopa.Movable.enabled = true;
            Koopa.Movable.Speed = Koopa.Profile.MoveSpeed;
            Koopa.Movable.Gravity = Koopa.Profile.FallSpeed;
            Koopa.Movable.MaxFallSpeed = Koopa.Profile.MaxFallSpeed;
            Koopa.Animator.SetTrigger("Idle");
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Koopa.ChangeDirectionToRight();
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Koopa.ChangeDirectionToLeft();
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController_OLD player)
        {
            Koopa.StateMachine.TransitionTo(Koopa.StateMachine.StateInShell);
            player.BounceJump();
        }
        public override void OnHittedByPlayerFromLeft(PlayerController_OLD player) => player.DamagePlayer();
        public override void OnHittedByPlayerFromRight(PlayerController_OLD player) => player.DamagePlayer();
        public override void OnHittedByPlayerFromBottom(PlayerController_OLD player) => player.DamagePlayer();
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