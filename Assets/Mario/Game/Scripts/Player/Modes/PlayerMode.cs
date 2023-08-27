namespace Mario.Game.Player
{
    public abstract class PlayerMode
    {
        #region Properties
        public PlayerStatedle StateIdle { get; protected set; }
        public PlayerStateRun StateRun { get; protected set; }
        public PlayerStateStop StateStop { get; protected set; }
        public PlayerStateJump StateJump { get; protected set; }
        public PlayerStateFall StateFall { get; protected set; }
        #endregion
    }
}