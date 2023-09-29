using Mario.Game.ScriptableObjects.Player;

namespace Mario.Game.Player
{
    public abstract class PlayerMode
    {
        #region Properties
        public PlayerModeProfile ModeProfile { get; set; }
        public PlayerStateIdle StateIdle { get; protected set; }
        public PlayerStateRun StateRun { get; protected set; }
        public PlayerStateStop StateStop { get; protected set; }
        public PlayerStateJump StateJump { get; protected set; }
        public PlayerStateFall StateFall { get; protected set; }
        public PlayerStateBuff StateBuff { get; protected set; }
        public PlayerStateNerf StateNerf { get; protected set; }
        public PlayerStateDeath StateDeath { get; protected set; }
        public PlayerStateTimeOut StateTimeOut { get; protected set; }
        public PlayerStateFlag StateFlag { get; protected set; }
        public PlayerStateDucking StateDucking { get; protected set; }
        public PlayerStateDuckingJump StateDuckingJump { get; protected set; }
        #endregion
    }
}