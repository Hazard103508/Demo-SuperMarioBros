namespace Mario.Game.Player
{
    public class PlayerStateSmallIdle : PlayerStatedle
    {
        #region Constructor
        public PlayerStateSmallIdle(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Animator.CrossFade("Small_Idle", 0);
        }
        #endregion
    }
}