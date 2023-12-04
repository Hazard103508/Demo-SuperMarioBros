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
        public override void Update()
        {
            if (SetTransitionToDucking())
                return;

            base.Update();
        }
        #endregion
    }
}