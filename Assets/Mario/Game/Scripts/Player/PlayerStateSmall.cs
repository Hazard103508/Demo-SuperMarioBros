using UnityEngine;

namespace Mario.Game.Player
{
    public abstract class PlayerStateSmall : PlayerState
    {
        #region Constructor
        public PlayerStateSmall(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override bool SetTransitionToIdle()
        {
            if (Player.Movable.Speed == 0)
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallIdle);
                return true;
            }
            return false;
        }
        protected override void SetTransitionToRun()
        {
            if (Player.InputActions.Move.x != 0)
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallRun);
        }
        protected override void SetTransitionToStop()
        {
            if (Player.InputActions.Move.x != 0 && Mathf.Sign(Player.Movable.Speed) != Mathf.Sign(Player.InputActions.Move.x))
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallStop);
        }
        protected override void SetTransitionToJump()
        {
            if (Player.InputActions.Jump)
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallJump);
        }
        #endregion
    }
}