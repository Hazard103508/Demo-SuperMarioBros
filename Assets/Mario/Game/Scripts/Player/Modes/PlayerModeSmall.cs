namespace Mario.Game.Player
{
    public class PlayerModeSmall: PlayerMode
    {
        public PlayerModeSmall(PlayerController player)
        {
            base.StateIdle = new PlayerStateSmallIdle(player);
            base.StateRun = new PlayerStateSmallRun(player);
            base.StateStop = new PlayerStateSmallStop(player);
            base.StateJump = new PlayerStateSmallJump(player);
            base.StateFall = new PlayerStateFall(player);
        }
    }
}