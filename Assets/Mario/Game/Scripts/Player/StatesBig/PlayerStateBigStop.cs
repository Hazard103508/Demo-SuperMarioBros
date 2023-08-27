namespace Mario.Game.Player
{
    public class PlayerStateBigStop : PlayerStateStop
    {
        #region Constructor
        public PlayerStateBigStop(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Big_Stop", 0);
        }
        #endregion
    }
}