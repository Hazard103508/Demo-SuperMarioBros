namespace Mario.Game.Player
{
    public class PlayerStateDuckingJump : PlayerStateJump
    {
        #region Constructor
        public PlayerStateDuckingJump(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Ducking";
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            SetRaycastDucking();
        }
        public override void Exit()
        {
            base.Exit();
            SetRaycastNormal();
        }
        #endregion
    }
}