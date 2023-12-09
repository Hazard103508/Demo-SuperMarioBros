using Mario.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateFall : PlayerState
    {
        #region Constructor
        public PlayerStateFall(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Public Methods
        public override void OnFall() => SetTransitionToDeathFall();
        public override void OnDeath() => SetTransitionToDeath();
        #endregion

        #region Protected Methods
        protected override bool SetTransitionToRun() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);
        protected override string GetAnimatorState()
        {
            return
                Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Run1 ? "Fall_Run1" :
                Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Run2 ? "Fall_Run2" :
                Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Run3 ? "Fall_Run3" :
                Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Jump ? "Jump" :
                Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Ducking ? "Ducking" :
                "";
        }
        #endregion

        #region IState Methods
        public override void Update()
        {
            SpeedUp();
            SpeedDown();
            ShootFireball();
        }
        public override void Exit()
        {
            base.Exit();
            SetRaycastNormal();
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToTop(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.SetJumpForce(0);
            HitObjectOnTop(hitInfo.hitObjects);
        }
        public override void OnHittedByMovingToBottom(RayHitInfo hitInfo)
        {
            HitObjectOnBottom(hitInfo.hitObjects);

            if (!hitInfo.IsBlock)
                return;

            if (SetTransitionToDucking())
                return;

            if (SetTransitionToIdle())
                return;

            SetTransitionToRun();
        }
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.Speed = 0;
            HitObjectOnLeft(hitInfo.hitObjects);
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.Speed = 0;
            HitObjectOnRight(hitInfo.hitObjects);
        }
        #endregion
    }
}