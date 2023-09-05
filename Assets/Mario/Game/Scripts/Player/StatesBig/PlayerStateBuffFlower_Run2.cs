namespace Mario.Game.Player
{
    public class PlayerStateBuffFlower_Run2 : PlayerStateBuffFlower
    {
        #region Constructor
        public PlayerStateBuffFlower_Run2(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Buff_Run2";
        #endregion
    }
}