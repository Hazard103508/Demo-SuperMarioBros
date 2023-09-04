namespace Mario.Game.Player
{
    public class PlayerStateBigIdle : PlayerStateIdle
    {
        #region Objects
        private readonly PlayerStateBuffFlower_Idle _flowerBuff;
        #endregion

        #region Constructor
        public PlayerStateBigIdle(PlayerController player) : base(player)
        {
            _flowerBuff = new PlayerStateBuffFlower_Idle(player);
        }
        #endregion

        #region Protected Methods
        protected override bool SetTransitionToBuff() => Player.StateMachine.TransitionTo(_flowerBuff);
        #endregion

        #region IState Methods
        public override void Update()
        {
            if (SetTransitionToDucking())
                return;

            base.Update();
        }
        #endregion

    }
}