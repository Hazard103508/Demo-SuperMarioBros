using Mario.Game.Interactable;
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
            Goomba.gameObject.layer = LayerMask.NameToLayer("NPC");
            Goomba.Movable.enabled = true;
            Goomba.Movable.Speed = Goomba.Profile.MoveSpeed;
            Goomba.Movable.Gravity = Goomba.Profile.FallSpeed;
            Goomba.Movable.MaxFallSpeed = Goomba.Profile.MaxFallSpeed;
            Goomba.Animator.SetTrigger("Idle");
        }
        public override void Update()
        {
            base.Update();
        }
        #endregion

        #region Private Methods
        private void KillGoomba(Vector3 hitPosition)
        {
            Goomba.StateMachine.TransitionTo(Goomba.StateMachine.StateDead);
            ChangeSpeedAfferHit(hitPosition);
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                ChangeDirectionToRight();
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                ChangeDirectionToLeft();
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player)
        {
            Goomba.StateMachine.TransitionTo(Goomba.StateMachine.StateHit);
            player.BounceJump();
        }
        public override void OnHittedByPlayerFromLeft(PlayerController player) => player.Hit();
        public override void OnHittedByPlayerFromRight(PlayerController player) => player.Hit();
        public override void OnHittedByPlayerFromBottom(PlayerController player) => player.Hit();
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