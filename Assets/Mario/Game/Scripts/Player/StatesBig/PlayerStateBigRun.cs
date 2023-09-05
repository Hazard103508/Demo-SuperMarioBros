namespace Mario.Game.Player
{
    public class PlayerStateBigRun : PlayerStateRun
    {
        #region Objects
        private readonly PlayerStateBuffFlower_Run1 _flowerBuff1;
        private readonly PlayerStateBuffFlower_Run2 _flowerBuff2;
        private readonly PlayerStateBuffFlower_Run3 _flowerBuff3;
        #endregion

        #region Constructor
        public PlayerStateBigRun(PlayerController player) : base(player)
        {
            _flowerBuff1 = new PlayerStateBuffFlower_Run1(player);
            _flowerBuff2 = new PlayerStateBuffFlower_Run2(player);
            _flowerBuff3 = new PlayerStateBuffFlower_Run3(player);
        }
        #endregion

        #region Protected Methods
        protected override bool SetTransitionToBuff()
        {
            PlayerStateBuffFlower nextBuffSate =
                Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Run1 ? _flowerBuff1:
                Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Run2 ? _flowerBuff2 :
                _flowerBuff3;

            return Player.StateMachine.TransitionTo(nextBuffSate);
        }
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