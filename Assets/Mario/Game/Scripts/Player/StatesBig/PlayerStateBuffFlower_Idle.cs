namespace Mario.Game.Player
{
    public class PlayerStateBuffFlower_Idle : PlayerStateBuffFlower
    {
        #region Constructor
        public PlayerStateBuffFlower_Idle(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Buff_Idle";
        #endregion
    }
}