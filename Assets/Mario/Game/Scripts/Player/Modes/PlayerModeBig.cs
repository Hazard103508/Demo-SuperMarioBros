namespace Mario.Game.Player
{
    public class PlayerModeBig : PlayerMode
    {
        public PlayerModeBig(PlayerController player)
        {
            base.StateIdle = new PlayerStateBigIdle(player);
            base.StateRun = new PlayerStateBigRun(player);
            base.StateStop = new PlayerStateBigStop(player);
            base.StateJump = new PlayerStateBigJump(player);
            base.StateFall = new PlayerStateFall(player);
        }
    }
}