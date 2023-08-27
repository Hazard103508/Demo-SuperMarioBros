namespace Mario.Game.Player
{
    public class PlayerStateSmallRun : PlayerStateRun
    {
        #region Constructor
        public PlayerStateSmallRun(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Animator.CrossFade("Small_Run", 0);
        }
        #endregion
    }
}