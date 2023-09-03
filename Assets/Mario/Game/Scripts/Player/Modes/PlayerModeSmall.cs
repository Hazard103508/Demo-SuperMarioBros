namespace Mario.Game.Player
{
    public class PlayerModeSmall : PlayerMode
    {
        public PlayerModeSmall(PlayerController player)
        {
            base.ModeProfile = player.Profile.Modes.Small;
            base.StateIdle = new PlayerStateIdle(player);
            base.StateRun = new PlayerStateRun(player);
            base.StateStop = new PlayerStateStop(player);
            base.StateJump = new PlayerStateJump(player);
            base.StateFall = new PlayerStateFall(player);
            base.StateBuff = new PlayerStateBuffMushroom(player);
            base.StateDeath = new PlayerStateDeath(player);
            base.StateFlag = new PlayerStateFlag(player);
        }
    }
}