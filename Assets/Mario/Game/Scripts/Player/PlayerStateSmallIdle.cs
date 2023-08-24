namespace Mario.Game.Player
{
    public class PlayerStateSmallIdle : PlayerStateSmall
    {
        #region Constructor
        public PlayerStateSmallIdle(PlayerController player) : base(player)
        {
        }
        #endregion

            #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Small_Idle", 0);
            Player.Movable.Gravity = Player.Profile.Fall.FallSpeed;
            Player.Movable.MaxFallSpeed = Player.Profile.Fall.MaxFallSpeed;
            ResetAnimationSpeed();
        }
        public override void Update()
        {
            SetTransitionToRun();
            SetTransitionToJump();
        }
        #endregion
    }
}