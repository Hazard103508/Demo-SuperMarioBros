namespace Mario.Game.Player
{
    public class PlayerStateBuffFlower_Run1 : PlayerStateBuffFlower
    {
        #region Constructor
        public PlayerStateBuffFlower_Run1(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Buff_Run1";
        #endregion
    }
}