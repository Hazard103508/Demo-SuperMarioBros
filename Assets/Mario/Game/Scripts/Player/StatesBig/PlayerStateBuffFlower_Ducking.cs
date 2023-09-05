namespace Mario.Game.Player
{
    public class PlayerStateBuffFlower_Ducking : PlayerStateBuffFlower
    {
        #region Constructor
        public PlayerStateBuffFlower_Ducking(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Buff_Ducking";
        #endregion
    }
}