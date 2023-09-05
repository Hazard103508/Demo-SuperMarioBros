namespace Mario.Game.Player
{
    public class PlayerStateBigDuckingJump : PlayerStateDuckingJump
    {
        #region Objects
        private readonly PlayerStateBuffFlower_Ducking _flowerBuff;
        #endregion

        #region Constructor
        public PlayerStateBigDuckingJump(PlayerController player) : base(player)
        {
            _flowerBuff = new PlayerStateBuffFlower_Ducking(player);
        }
        #endregion

        #region Protected Methods
        protected override bool SetTransitionToBuff() => Player.StateMachine.TransitionTo(_flowerBuff);
        #endregion
    }
}