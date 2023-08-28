using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateRun : PlayerState
    {
        #region Objects
        private bool _jumpWasPressed;
        #endregion

        #region Constructor
        public PlayerStateRun(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Public Methods
        public override void OnBuff() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateBuff);
        #endregion

        #region Private Methods
        private bool SetTransitionToJump()
        {
            if (!_jumpWasPressed && Player.InputActions.Jump)
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateJump);
                return true;
            }

            _jumpWasPressed = Player.InputActions.Jump;
            return false;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Run", 0);
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

            if (SetTransitionToJump())
                return;

            SetTransitionToStop();
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