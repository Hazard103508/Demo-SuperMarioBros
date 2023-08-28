namespace Mario.Game.Player
{
    public class PlayerStateIdle : PlayerState
    {
        #region Constructor
        public PlayerStateIdle(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Public Methods
        public override void OnBuff() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateBuff);
        #endregion


        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Animator.CrossFade("Idle", 0);
            Player.Movable.Speed = 0;
            Player.Movable.Gravity = Player.Profile.Fall.FallSpeed;
            Player.Movable.MaxFallSpeed = Player.Profile.Fall.MaxFallSpeed;
            ResetAnimationSpeed();
        }
        public override void Update()
        {
            if (SetTransitionToRun())
                return;

            SetTransitionToJump();
        }
        #endregion
    }
}