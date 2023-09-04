namespace Mario.Game.Player
{
    public class PlayerStateBuffFlower_Jump : PlayerStateBuffFlower
    {
        #region Constructor
        public PlayerStateBuffFlower_Jump(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Buff_Jump";
        #endregion
    }
}