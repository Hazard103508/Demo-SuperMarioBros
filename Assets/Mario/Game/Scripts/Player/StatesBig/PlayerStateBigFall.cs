namespace Mario.Game.Player
{
    public class PlayerStateBigFall : PlayerStateFall
    {
        #region Objects
        private readonly PlayerStateBuffFlower_Jump _flowerBuffJump;
        private readonly PlayerStateBuffFlower_Run1 _flowerBuffRun1;
        private readonly PlayerStateBuffFlower_Run2 _flowerBuffRun2;
        private readonly PlayerStateBuffFlower_Run3 _flowerBuffRun3;
        private readonly PlayerStateBuffFlower_Ducking _flowerBuffDucking;
        #endregion

        #region Constructor
        public PlayerStateBigFall(PlayerController player) : base(player)
        {
            _flowerBuffRun1 = new PlayerStateBuffFlower_Run1(player);
            _flowerBuffRun2 = new PlayerStateBuffFlower_Run2(player);
            _flowerBuffRun3 = new PlayerStateBuffFlower_Run3(player);
            _flowerBuffJump = new PlayerStateBuffFlower_Jump(player);
            _flowerBuffDucking = new PlayerStateBuffFlower_Ducking(player);
        }
        #endregion

        #region Protected Methods
        protected override bool SetTransitionToBuff()
        {
            PlayerStateBuffFlower nextBuffSate =
                Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Run1 ? _flowerBuffRun1 :
                Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Run2 ? _flowerBuffRun2 :
                Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Run3 ? _flowerBuffRun3 :
                Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Ducking ? _flowerBuffDucking :
                _flowerBuffJump;

            return Player.StateMachine.TransitionTo(nextBuffSate);
        }
        #endregion
    }
}