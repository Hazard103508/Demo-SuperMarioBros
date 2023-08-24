using Mario.Game.Commons;

namespace Mario.Game.Player
{
    public class PlayerStateMachine : StateMachine
    {
        #region Properties
        new public PlayerState CurrentState => (PlayerState)base.CurrentState;
        public PlayerStateSmallIdle StateSmallIdle { get; private set; }
        public PlayerStateSmallRun StateSmallRun { get; private set; }
        public PlayerStateSmallStop StateSmallStop { get; private set; }
        public PlayerStateSmallJump StateSmallJump { get; private set; }
        public PlayerStateSmallFall StateSmallFall { get; private set; }
        #endregion

        #region Constructor
        public PlayerStateMachine(PlayerController Player)
        {
            StateSmallIdle = new PlayerStateSmallIdle(Player);
            StateSmallRun = new PlayerStateSmallRun(Player);
            StateSmallStop = new PlayerStateSmallStop(Player);
            StateSmallJump = new PlayerStateSmallJump(Player);
            StateSmallFall = new PlayerStateSmallFall(Player);
        }
        #endregion
    }
}