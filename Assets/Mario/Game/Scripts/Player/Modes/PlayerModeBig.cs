namespace Mario.Game.Player
{
    public class PlayerModeBig : PlayerMode
    {
        public PlayerModeBig(PlayerController player)
        {
            base.ModeProfile = player.Profile.Modes.Big;
            base.StateIdle = new PlayerStateBigIdle(player);
            base.StateRun = new PlayerStateBigRun(player);
            base.StateStop = new PlayerStateBigStop(player);
            base.StateJump = new PlayerStateBigJump(player);
            base.StateFall = new PlayerStateFall(player);
            //base.StateBuff -- custom buff state in each state
            base.StateNerf = new PlayerStateBigNerf(player);
            base.StateDeath = new PlayerStateDeath(player);
            base.StateFlag = new PlayerStateFlag(player);
            base.StateDucking = new PlayerStateDucking(player);
            base.StateDuckingJump = new PlayerStateDuckingJump(player);
        }
    }
}