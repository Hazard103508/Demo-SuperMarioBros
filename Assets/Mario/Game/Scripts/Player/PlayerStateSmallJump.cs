namespace Mario.Game.Player
{
    public class PlayerStateSmallJump : PlayerStateSmall
    {
        #region Constructor
        public PlayerStateSmallJump(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Small_Jump", 0);
        }
        #endregion
    }
}