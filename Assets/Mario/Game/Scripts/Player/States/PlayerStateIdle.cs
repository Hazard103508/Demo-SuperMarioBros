namespace Mario.Game.Player
{
    public class PlayerStateIdle : PlayerState
    {
        #region Objects
        private bool _jumpWasPressed;
        #endregion

        #region Constructor
        public PlayerStateIdle(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Public Methods
        public override void OnBuff() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateBuff);
        #endregion

        #region Private Methods
        private void SetTransitionToJump()
        {
            if (!_jumpWasPressed && Player.InputActions.Jump)
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateJump);

            _jumpWasPressed = Player.InputActions.Jump;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Idle", 0);
            Player.Movable.Speed = 0;
            Player.Movable.Gravity = Player.Profile.Fall.FallSpeed;
            Player.Movable.MaxFallSpeed = Player.Profile.Fall.MaxFallSpeed;
            ResetAnimationSpeed();

            _jumpWasPressed = Player.InputActions.Jump;
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