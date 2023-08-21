using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Goomba
{
    public class GoombaStateWalk : GoombaState
    {
        #region Constructor
        public GoombaStateWalk(Goomba goomba) : base(goomba) 
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Goomba.Movable.enabled = true;
            Goomba.Movable.Speed = Goomba.Profile.MoveSpeed;
            Goomba.Movable.Gravity = Goomba.Profile.FallSpeed;
            Goomba.Movable.MaxFallSpeed = Goomba.Profile.MaxFallSpeed;
            Goomba.Animator.SetTrigger("Idle");
        }
        #endregion

        #region Private Methods
        private void KillGoomba(Vector3 hitPosition)
        {
            Goomba.StateMachine.TransitionTo(Goomba.StateMachine.StateDead);
            Goomba.ChangeSpeedAfferHit(hitPosition);
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Goomba.ChangeDirectionToRight();
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Goomba.ChangeDirectionToLeft();
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController_OLD player)
        {
            Goomba.StateMachine.TransitionTo(Goomba.StateMachine.StateHit);
            player.BounceJump();
        }
        public override void OnHittedByPlayerFromLeft(PlayerController_OLD player) => player.DamagePlayer();
        public override void OnHittedByPlayerFromRight(PlayerController_OLD player) => player.DamagePlayer();
        public override void OnHittedByPlayerFromBottom(PlayerController_OLD player) => player.DamagePlayer();
        #endregion

        #region On Box Hit
        public override void OnHittedByBox(GameObject box) => KillGoomba(box.transform.position);
        #endregion

        #region On Koopa Hit
        public override void OnHittedByKoppa(Koopa.Koopa koopa) => KillGoomba(koopa.transform.position);
        #endregion

        #region On Fireball Hit
        public override void OnHittedByFireBall(Fireball fireball) => KillGoomba(fireball.transform.position);
        #endregion
    }
}