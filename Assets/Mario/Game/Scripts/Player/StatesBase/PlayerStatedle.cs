namespace Mario.Game.Player
{
    public abstract class PlayerStatedle : PlayerState
    {
        #region Objects
        private bool _jumpWasPressed;
        #endregion

        #region Constructor
        public PlayerStatedle(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Private Methods
        private void SetTransitionToRun()
        {
            if (Player.InputActions.Move.x != 0)
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallRun);
        }
        private void SetTransitionToJump()
        {
            if (!_jumpWasPressed && Player.InputActions.Jump)
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallJump);

            _jumpWasPressed = Player.InputActions.Jump;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Movable.Speed = 0;
            Player.Movable.Gravity = Player.Profile.Fall.FallSpeed;
            Player.Movable.MaxFallSpeed = Player.Profile.Fall.MaxFallSpeed;
            ResetAnimationSpeed();

            _jumpWasPressed = Player.InputActions.Jump;
        }
        public override void Update()
        {
            SetTransitionToRun();
            SetTransitionToJump();
        }
        #endregion
    }
}