namespace Mario.Game.Player
{
    public class PlayerStateBigStop : PlayerStateStop
    {
        #region Objects
        private readonly PlayerStateBuffFlower_Stop _flowerBuff;
        #endregion

        #region Constructor
        public PlayerStateBigStop(PlayerController player) : base(player)
        {
            _flowerBuff = new PlayerStateBuffFlower_Stop(player);
        }
        #endregion

        #region Protected Methods
        protected override bool SetTransitionToBuff() => Player.StateMachine.TransitionTo(_flowerBuff);
        #endregion
    }
}