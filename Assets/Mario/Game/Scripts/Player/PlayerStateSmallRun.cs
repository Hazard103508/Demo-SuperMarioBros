using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateSmallRun : PlayerStateSmall
    {
        #region Objects
        private bool _jumpWasPressed;
        #endregion

        #region Constructor
        public PlayerStateSmallRun(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override void SetTransitionToJump()
        {
            if (!_jumpWasPressed)
                base.SetTransitionToJump();

            _jumpWasPressed = Player.InputActions.Jump;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Small_Run", 0);
            _jumpWasPressed = Player.InputActions.Jump;
        }
        public override void Update()
        {
            SpeedUp();
            SpeedDown();

            if (SetTransitionToIdle())
                return;

            SetAnimationSpeed();
            SetSpriteDirection();

            SetTransitionToStop();
            SetTransitionToJump();
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