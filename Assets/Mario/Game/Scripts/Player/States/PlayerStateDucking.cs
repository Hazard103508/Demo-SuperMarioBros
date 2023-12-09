using Mario.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateDucking : PlayerState
    {
        #region Constructor
        public PlayerStateDucking(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Public Methods
        public override void OnFall() => SetTransitionToDeathFall();
        public override void OnDeath() => SetTransitionToDeath();
        #endregion

        #region protected Methods
        protected override string GetAnimatorState() => "Ducking";
        protected override bool SetTransitionToRun()
        {
            if (Player.Movable.Speed != 0)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);

            return false;
        }
        protected override bool SetTransitionToIdle() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateIdle);
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            SetRaycastDucking();
        }
        public override void Update()
        {
            SpeedDown();

            if (SetTransitionToDuckingJump())
                return;

            if (Player.InputActions.Ducking && Player.InputActions.Move == 0)
                return;

            if (Player.Movable.JumpForce != 0)
                return;

            if (SetTransitionToRun())
                return;

            SetTransitionToIdle();
        }
        public override void Exit()
        {
            base.Exit();
            SetRaycastNormal();
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToTop(RayHitInfo hitInfo) => HitObjectOnTop(hitInfo.hitObjects);
        public override void OnHittedByMovingToBottom(RayHitInfo hitInfo) => HitObjectOnBottom(hitInfo.hitObjects);
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo) => HitObjectOnLeft(hitInfo.hitObjects);
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo) => HitObjectOnRight(hitInfo.hitObjects);
        #endregion
    }
}