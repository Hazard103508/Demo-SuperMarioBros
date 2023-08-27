using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public abstract class PlayerStateRun : PlayerState
    {
        #region Objects
        private bool _jumpWasPressed;
        #endregion

        #region Constructor
        public PlayerStateRun(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Private Methods
        private bool SetTransitionToIdle()
        {
            if (Player.Movable.Speed == 0)
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateIdle);
                return true;
            }
            return false;
        }
        private void SetTransitionToJump()
        {
            if (!_jumpWasPressed && Player.InputActions.Jump)
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateJump);

            _jumpWasPressed = Player.InputActions.Jump;
        }
        private bool SetTransitionToStop()
        {
            if (Player.InputActions.Move.x != 0 && Mathf.Sign(Player.Movable.Speed) != Mathf.Sign(Player.InputActions.Move.x))
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateStop);
                return true;
            }

            return false;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
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

            if (SetTransitionToStop())
                return;

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