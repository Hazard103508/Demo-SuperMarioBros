namespace Mario.Game.Player
{
    public class PlayerStateBuffFlower_Run3 : PlayerStateBuffFlower
    {
        #region Constructor
        public PlayerStateBuffFlower_Run3(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Buff_Run3";
        #endregion
    }
}