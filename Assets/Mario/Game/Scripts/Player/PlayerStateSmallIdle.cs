namespace Mario.Game.Player
{
    public class PlayerStateSmallIdle : PlayerState
    {
        #region Constructor
        public PlayerStateSmallIdle(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Private Methods
        private void TransitionToIdle()
        {
            if (Player.InputActions.Move.x != 0)
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallRun);
        }
        #endregion

            #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Small_Idle", 0);
            Player.Movable.Gravity = Player.Profile.Fall.FallSpeed;
            Player.Movable.MaxFallSpeed = Player.Profile.Fall.MaxFallSpeed;
        }
        public override void Update()
        {
            TransitionToIdle();
        }
        #endregion
    }
}