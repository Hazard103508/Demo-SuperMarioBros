namespace Mario.Game.Player
{
    public class PlayerStateBigJump : PlayerStateJump
    {
        #region Objects
        private readonly PlayerStateBuffFlower_Jump _flowerBuff;
        #endregion

        #region Constructor
        public PlayerStateBigJump(PlayerController player) : base(player)
        {
            _flowerBuff = new PlayerStateBuffFlower_Jump(player);
        }
        #endregion

        #region Protected Methods
        protected override bool SetTransitionToBuff() => Player.StateMachine.TransitionTo(_flowerBuff);
        #endregion
    }
}