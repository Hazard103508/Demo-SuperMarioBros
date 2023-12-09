namespace Mario.Game.Player
{
    public class PlayerModeSuper : PlayerMode
    {
        public PlayerModeSuper(PlayerController player) : base()
        {
            base.ModeProfile = PlayerService.PlayerProfile.Modes.Super;
            base.StateIdle = new PlayerStateSuperIdle(player);
            base.StateRun = new PlayerStateSuperRun(player);
            base.StateStop = new PlayerStateStop(player);
            base.StateJump = new PlayerStateJump(player);
            base.StateFall = new PlayerStateFall(player);
            base.StateDeath = new PlayerStateDeath(player);
            base.StateTimeOut = new PlayerStateTimeOut(player);
            base.StateDeathFall = new PlayerStateDeathFall(player);
            base.StateFlag = new PlayerStateFlag(player);
            base.StateDucking = new PlayerStateDucking(player);
            base.StateDuckingJump = new PlayerStateDuckingJump(player);
            base.StateDuckingFall = new PlayerStateDuckingFall(player);
            base.StateFire = new PlayerStateFire(player);
        }
    }
}