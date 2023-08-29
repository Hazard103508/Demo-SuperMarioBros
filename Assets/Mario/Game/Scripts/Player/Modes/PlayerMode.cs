namespace Mario.Game.Player
{
    public abstract class PlayerMode
    {
        #region Properties
        public PlayerStateIdle StateIdle { get; protected set; }
        public PlayerStateRun StateRun { get; protected set; }
        public PlayerStateStop StateStop { get; protected set; }
        public PlayerStateJump StateJump { get; protected set; }
        public PlayerStateFall StateFall { get; protected set; }
        public PlayerStateBuff StateBuff { get; protected set; }
        public PlayerStateNerf StateNerf { get; protected set; }
        #endregion
    }
}