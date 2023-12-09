using Mario.Commons.Structs;

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
        public override void OnFall() => SetTransitionToDeathFall();
        public override void OnDeath() => SetTransitionToDeath();
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Idle";
        protected override bool SetTransitionToFall()
        {
            if (base.SetTransitionToFall())
            {
                Player.Animator.CrossFade("Jump", 0);
                return true;
            }
            return false;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Movable.Speed = 0;
            Player.Movable.Gravity = Player.StateMachine.CurrentMode.ModeProfile.Fall.NormalSpeed;
            Player.Movable.MaxFallSpeed = Player.StateMachine.CurrentMode.ModeProfile.Fall.MaxFallSpeed;
            ResetAnimationSpeed();
        }
        public override void Update()
        {
            if (SetTransitionToRun())
                return;

            SetTransitionToJump();

            SetTransitionToFall();

            ShootFireball();
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