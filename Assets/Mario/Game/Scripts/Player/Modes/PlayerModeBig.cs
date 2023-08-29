namespace Mario.Game.Player
{
    public class PlayerModeBig : PlayerMode
    {
        public PlayerModeBig(PlayerController player)
        {
            base.StateIdle = new PlayerStateIdle(player);
            base.StateRun = new PlayerStateRun(player);
            base.StateStop = new PlayerStateStop(player);
            base.StateJump = new PlayerStateJump(player);
            base.StateFall = new PlayerStateFall(player);
            //base.StateBuff= new PlayerStateBuff(player);
            base.StateNerf = new PlayerStateBigNerf(player);
            base.StateDeath = new PlayerStateDeath(player);
        }
    }
}