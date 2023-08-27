namespace Mario.Game.Player
{
    public class PlayerStateSmallStop : PlayerStateStop
    {
        #region Constructor
        public PlayerStateSmallStop(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Small_Stop", 0);
        }
        #endregion
    }
}