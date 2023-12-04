namespace Mario.Game.Player
{
    public class PlayerStateBigIdle : PlayerStateIdle
    {
        #region Constructor
        public PlayerStateBigIdle(PlayerController player) : base(player)
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