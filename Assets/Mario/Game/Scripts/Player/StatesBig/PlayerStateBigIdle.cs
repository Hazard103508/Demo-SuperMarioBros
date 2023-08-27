namespace Mario.Game.Player
{
    public class PlayerStateBigIdle : PlayerStatedle
    {
        #region Constructor
        public PlayerStateBigIdle(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Animator.CrossFade("Big_Idle", 0);
        }
        #endregion
    }
}