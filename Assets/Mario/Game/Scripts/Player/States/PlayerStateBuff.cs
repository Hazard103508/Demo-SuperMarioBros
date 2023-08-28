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
        protected override bool SetTransitionToJump()
        {
            if (Player.Movable.JumpForce > 0)
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateJump);
                return true;
            }

            return false;
        }
        protected override bool SetTransitionToFall()
        {
            if (Player.Movable.JumpForce < 0)
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateFall);
                return true;
            }

            return false;
        }
        protected void SetNextState()
        {
            // -------------------------------------------------
            // esto no anda....
            // ver como guardar el estado anteior y volverlo a asignar
            if (SetTransitionToJump())
                return;

            if (SetTransitionToFall())
                return;

            if (SetTransitionToStop())
                return;
            // -------------------------------------------------

            if (SetTransitionToRun())
                return;

            SetTransitionToIdle();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Movable.enabled = false;
            Player.Animator.CrossFade("Buff", 0);
        }
        #endregion
    }
}