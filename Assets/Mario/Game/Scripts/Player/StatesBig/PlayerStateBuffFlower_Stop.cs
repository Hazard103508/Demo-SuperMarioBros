namespace Mario.Game.Player
{
    public class PlayerStateBuffFlower_Stop : PlayerStateBuffFlower
    {
        #region Constructor
        public PlayerStateBuffFlower_Stop(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Buff_Stop";
        #endregion
    }
}