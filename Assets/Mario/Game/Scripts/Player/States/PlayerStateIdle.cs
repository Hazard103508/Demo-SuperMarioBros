namespace Mario.Game.Player
{
    public class PlayerStateIdle : PlayerState
    {
        #region Constructor
        public PlayerStateIdle(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Public Methods
        public override void OnBuff() => SetTransitionToBuff();
        public override void OnNerf() => SetTransitionToNerf();
        public override void OnDeath() => SetTransitionToDeath();
        public override void OnTouchFlag() => SetTransitionToFlag();
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Idle";
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Movable.Speed = 0;
            Player.Movable.Gravity = Player.StateMachine.CurrentMode.ModeProfile.Fall.NormalSpeed;
            Player.Movable.MaxFallSpeed = Player.StateMachine.CurrentMode.ModeProfile.Fall.MaxFallSpeed;
            ResetAnimationSpeed();
        }
        public override void Update()
        {
            if (SetTransitionToRun())
                return;

            SetTransitionToJump();
        }
        #endregion
    }
}