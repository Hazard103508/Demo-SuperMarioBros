namespace Mario.Game.Player
{
    public class PlayerStateBigDucking : PlayerStateDucking
    {
        #region Objects
        private readonly PlayerStateBuffFlower_Ducking _flowerBuff;
        #endregion

        #region Constructor
        public PlayerStateBigDucking(PlayerController player) : base(player)
        {
            _flowerBuff = new PlayerStateBuffFlower_Ducking(player);
        }
        #endregion

        #region Protected Methods
        protected override bool SetTransitionToBuff() => Player.StateMachine.TransitionTo(_flowerBuff);
        #endregion
    }
}