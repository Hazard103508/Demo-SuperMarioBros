using Mario.Game.Commons;

namespace Mario.Game.Player
{
    public class PlayerStateMachine : StateMachine
    {
        #region Properties
        public PlayerMode CurrentMode { get; private set; }
        public PlayerMode ModeIdle { get; private set; }
        public PlayerMode ModeBig { get; private set; }
        public PlayerMode ModeSuper { get; private set; }

        new public PlayerState CurrentState => (PlayerState)base.CurrentState;
        #endregion

        #region Constructor
        public PlayerStateMachine(PlayerController Player)
        {
            ModeIdle = new PlayerModeSmall(Player);
            ModeBig = new PlayerModeBig(Player);
            //ModeSuper = new PlayerModeSuper(Player);

            //CurrentMode = ModeIdle;
            CurrentMode = ModeBig;
        }
        #endregion
    }
}