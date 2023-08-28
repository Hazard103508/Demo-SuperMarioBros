using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateStop : PlayerState
    {
        #region Constructor
        public PlayerStateStop(PlayerController player) : base(player)
        {
        }
        #endregion

        #region protected Methods
        protected override void SetSpriteDirection() => Player.Renderer.flipX = Player.Movable.Speed > 0;
        protected override bool SetTransitionToRun()
        {
            if (Mathf.Sign(Player.Movable.Speed) == Mathf.Sign(Player.InputActions.Move.x))
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);
                return true;
            }
            return false;
        }

        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Animator.CrossFade("Stop", 0);
        }
        public override void Update()
        {
            SpeedUp();
            SpeedDown();

            if (SetTransitionToIdle())
                return;

            SetSpriteDirection();

            if (SetTransitionToJump())
                return;

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