using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Player;

namespace Mario.Game.Player
{
    public abstract class PlayerMode
    {
        protected IPlayerService PlayerService;

        public PlayerMode()
        {
            PlayerService = ServiceLocator.Current.Get<IPlayerService>();
        }

        public PlayerModeProfile ModeProfile { get; set; }
        public PlayerStateIdle StateIdle { get; protected set; }
        public PlayerStateRun StateRun { get; protected set; }
        public PlayerStateStop StateStop { get; protected set; }
        public PlayerStateJump StateJump { get; protected set; }
        public PlayerStateFall StateFall { get; protected set; }
        public PlayerStateDeath StateDeath { get; protected set; }
        public PlayerStateTimeOut StateTimeOut { get; protected set; }
        public PlayerStateDeathFall StateDeathFall { get; protected set; }
        public PlayerStateFlag StateFlag { get; protected set; }
        public PlayerStateDucking StateDucking { get; protected set; }
        public PlayerStateDuckingJump StateDuckingJump { get; protected set; }
        public PlayerStateDuckingFall StateDuckingFall { get; protected set; }
        public PlayerStateFire StateFire { get; protected set; }
    }
}