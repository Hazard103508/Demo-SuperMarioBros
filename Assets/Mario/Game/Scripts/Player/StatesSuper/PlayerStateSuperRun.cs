namespace Mario.Game.Player
{
    public class PlayerStateSuperRun : PlayerStateRun
    {
        #region Constructor
        public PlayerStateSuperRun(PlayerController player) : base(player)
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