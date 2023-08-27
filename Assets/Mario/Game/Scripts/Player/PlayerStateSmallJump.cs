using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateSmallJump : PlayerStateSmall
    {
        #region Objects
        private float _jumpForce;
        private float _initYPos;
        private float _maxHeight;
        #endregion

        #region Constructor
        public PlayerStateSmallJump(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Private Methods
        private void Jump()
        {
            var jumpHeight = UnityShared.Helpers.MathEquations.Trajectory.GetHeight(_jumpForce, -Player.Movable.Gravity);
            var jumpedHeight = Player.transform.position.y - _initYPos;
            var currentJump = jumpHeight + jumpedHeight;
            if (currentJump < _maxHeight)
                Player.Movable.AddJumpForce(_jumpForce);
            else
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallFall);
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Small_Jump", 0);

            _initYPos = Player.transform.position.y;
            _maxHeight = Player.Profile.Jump.MaxIdleHeight;
            _jumpForce = UnityShared.Helpers.MathEquations.Trajectory.GetVelocity(Player.Profile.Jump.MinHeight, -Player.Movable.Gravity);
            Player.Movable.AddJumpForce(_jumpForce);
        }
        public override void Update()
        {
            if (!Player.InputActions.Jump)
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallFall);
                return;
            }

            Jump();
        }
        #endregion
    }
}