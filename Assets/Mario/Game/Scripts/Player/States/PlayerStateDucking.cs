namespace Mario.Game.Player
{
    public class PlayerStateDucking : PlayerState
    {
        #region Constructor
        public PlayerStateDucking(PlayerController player) : base(player)
        {
        }
        #endregion

        #region protected Methods
        protected override bool SetTransitionToRun()
        {
            if (Player.Movable.Speed != 0)
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);
                return true;
            }
            return false;
        }
        protected override bool SetTransitionToIdle()
        {
            Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateIdle);
            return true;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Animator.CrossFade("Ducking", 0);
            SetRaycastDucking();
        }
        public override void Update()
        {
            SpeedDown();

            if (Player.InputActions.Ducking)
                return;

            if (SetTransitionToRun())
                return;

            SetTransitionToIdle();
        }
        public override void Exit()
        {
            base.Exit();
            SetRaycastNormal();
        }
        #endregion
    }
}