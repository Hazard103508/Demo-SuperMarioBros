namespace Mario.Game.Player
{
    public class PlayerStateTimeOut : PlayerStateDeath
    {
        #region Constructor
        public PlayerStateTimeOut(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "TimeOut";
        #endregion
    }
}