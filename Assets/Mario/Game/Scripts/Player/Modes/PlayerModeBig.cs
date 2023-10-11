namespace Mario.Game.Player
{
    public class PlayerModeBig : PlayerMode
    {
        public PlayerModeBig(PlayerController player) : base()
        {
            base.ModeProfile = PlayerService.PlayerProfile.Modes.Big;
            base.StateIdle = new PlayerStateBigIdle(player);
            base.StateRun = new PlayerStateBigRun(player);
            base.StateStop = new PlayerStateBigStop(player);
            base.StateJump = new PlayerStateBigJump(player);
            base.StateFall = new PlayerStateBigFall(player);
            //base.StateBuff -- custom buff state in each state
            base.StateNerf = new PlayerStateBigNerf(player);
            base.StateDeath = new PlayerStateDeath(player);
            base.StateTimeOut = new PlayerStateTimeOut(player);
            base.StateFlag = new PlayerStateFlag(player);
            base.StateDucking = new PlayerStateBigDucking(player);
            base.StateDuckingJump = new PlayerStateBigDuckingJump(player);
        }
    }
}