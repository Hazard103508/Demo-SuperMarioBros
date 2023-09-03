namespace Mario.Game.Player
{
    public class PlayerStateDucking : PlayerState
    {
        #region Constructor
        public PlayerStateDucking(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Public Methods
        public override void OnBuff() => SetTransitionToBuff();
        public override void OnNerf() => SetTransitionToNerf();
        public override void OnDeath() => SetTransitionToDeath();
        public override void OnTouchFlag() => SetTransitionToFlag();
        #endregion

        #region protected Methods
        protected override string GetAnimatorState() => "Ducking";
        protected override bool SetTransitionToRun()
        {
            if (Player.Movable.Speed != 0)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);

            return false;
        }
        protected override bool SetTransitionToIdle() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateIdle);
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            SetRaycastDucking();
        }
        public override void Update()
        {
            SpeedDown();

            if (SetTransitionToDuckingJump())
                return;

            if (Player.InputActions.Ducking && Player.InputActions.Move.x == 0)
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