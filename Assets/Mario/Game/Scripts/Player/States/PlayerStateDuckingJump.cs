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
        protected override bool SetTransitionToFall()
        {
            if (!Player.InputActions.Jump || Player.Movable.JumpForce <= 0)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateDuckingFall);

            return false;
        }
        protected override string GetAnimatorState() => "Ducking";
        protected override void ShootFireball()
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            SetRaycastDucking();
        }
        #endregion
    }
}