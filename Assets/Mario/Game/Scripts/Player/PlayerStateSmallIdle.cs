namespace Mario.Game.Player
{
    public class PlayerStateSmallIdle : PlayerStateSmall
    {
        #region Objects
        private bool _jumpWasPressed;
        #endregion

        #region Constructor
        public PlayerStateSmallIdle(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Small_Idle", 0);
            Player.Movable.Speed = 0;
            Player.Movable.Gravity = Player.Profile.Fall.FallSpeed;
            Player.Movable.MaxFallSpeed = Player.Profile.Fall.MaxFallSpeed;
            ResetAnimationSpeed();

            _jumpWasPressed = Player.InputActions.Jump;
        }
        public override void Update()
        {
            SetTransitionToRun();

            if (!_jumpWasPressed)
                SetTransitionToJump();

            _jumpWasPressed = Player.InputActions.Jump;
        }
        #endregion
    }
}