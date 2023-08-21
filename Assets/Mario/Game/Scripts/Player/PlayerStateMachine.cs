using Mario.Game.Commons;

namespace Mario.Game.Player
{
    public class PlayerStateMachine : StateMachine
    {
        #region Properties
        new public PlayerState CurrentState => (PlayerState)base.CurrentState;
        public PlayerStateSmallIdle StateSmallIdle { get; private set; }
        public PlayerStateSmallWalk StateSmallWalk { get; private set; }
        #endregion

        #region Constructor
        public PlayerStateMachine(PlayerController Player)
        {
            StateSmallIdle = new PlayerStateSmallIdle(Player);
            StateSmallWalk = new PlayerStateSmallWalk(Player);
        }
        #endregion
    }
}