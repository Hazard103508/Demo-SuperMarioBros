namespace Mario.Game.Player
{
    public class PlayerModeBig : PlayerMode
    {
        public PlayerModeBig(PlayerController player) : base()
        {
            base.ModeProfile = PlayerService.PlayerProfile.Modes.Big;
            base.StateIdle = new PlayerStateBigIdle(player);
            base.StateRun = new PlayerStateBigRun(player);
            base.StateStop = new PlayerStateStop(player);
            base.StateJump = new PlayerStateJump(player);
            base.StateFall = new PlayerStateFall(player);
            //base.StateBuff -- custom buff state in each state
            //base.StateNerf = new PlayerStateBigNerf(player);
            base.StateDeath = new PlayerStateDeath(player);
            base.StateTimeOut = new PlayerStateTimeOut(player);
            base.StateDeathFall = new PlayerStateDeathFall(player);
            base.StateFlag = new PlayerStateFlag(player);
            base.StateDucking = new PlayerStateDucking(player);
            base.StateDuckingJump = new PlayerStateDuckingJump(player);
            base.StateDuckingFall = new PlayerStateDuckingFall(player);
        }
    }
}