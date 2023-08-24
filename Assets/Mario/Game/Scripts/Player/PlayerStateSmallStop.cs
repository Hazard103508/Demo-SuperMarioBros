using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateSmallStop : PlayerStateSmall
    {
        #region Constructor
        public PlayerStateSmallStop(PlayerController player) : base(player)
        {
        }
        #endregion

        #region protected Methods
        protected override void SetSpriteDirection() => Player.Renderer.flipX = Player.Movable.Speed > 0;
        protected override void SetTransitionToRun()
        {
            if (Mathf.Sign(Player.Movable.Speed) == Mathf.Sign(Player.InputActions.Move.x))
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallRun);
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Small_Stop", 0);
        }
        public override void Update()
        {
            SpeedDown();

            if (SetTransitionToIdle())
                return;

            SetSpriteDirection();
            SetTransitionToRun();
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.Speed = 0;
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.Speed = 0;
        }
        #endregion
    }
}