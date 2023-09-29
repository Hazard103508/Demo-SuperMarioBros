namespace Mario.Game.Player
{
    public class PlayerModeSuper : PlayerMode
    {
        public PlayerModeSuper(PlayerController player)
        {
            base.ModeProfile = player.Profile.Modes.Super;
            base.StateIdle = new PlayerStateSuperIdle(player);
            base.StateRun = new PlayerStateSuperRun(player);
            base.StateStop = new PlayerStateStop(player);
            base.StateJump = new PlayerStateJump(player);
            base.StateFall = new PlayerStateFall(player);
            base.StateNerf = new PlayerStateBigNerf(player); 
            base.StateTimeOut = new PlayerStateTimeOut(player);
            base.StateFlag = new PlayerStateFlag(player);
            base.StateDucking = new PlayerStateDucking(player);
            base.StateDuckingJump = new PlayerStateDuckingJump(player);
        }
    }
}