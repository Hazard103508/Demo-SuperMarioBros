namespace Mario.Game.Player
{
    public class PlayerStateBigRun : PlayerStateRun
    {
        #region Constructor
        public PlayerStateBigRun(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Animator.CrossFade("Big_Run", 0);
        }
        #endregion
    }
}