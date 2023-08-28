using UnityEngine;

namespace Mario.Game.Player
{
    public abstract class PlayerStateBuff : PlayerState
    {
        #region Constructor
        public PlayerStateBuff(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override bool SetTransitionToIdle()
        {
            Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateIdle);
            return true;
        }
        protected override bool SetTransitionToRun()
        {
            if (Player.Movable.Speed != 0)
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);
                return true;
            }
            return false;
        }
        protected void SetNextState()
        {
            if (SetTransitionToStop())
                return;

            if (SetTransitionToRun())
                return;

            SetTransitionToIdle();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Buff", 0);
        }
        #endregion
    }
}