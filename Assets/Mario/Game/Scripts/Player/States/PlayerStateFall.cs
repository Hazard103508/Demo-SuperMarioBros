using UnityShared.Commons.Structs;

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
        public override void OnBuff() => SetTransitionToBuff();
        public override void OnNerf() => SetTransitionToNerf();
        public override void OnDeath() => SetTransitionToDeath();
        public override void OnTimeOut() => SetTransitionToTimeOut();
        public override void OnTouchFlag() => SetTransitionToFlag();
        #endregion

        #region Protected Methods
        protected override bool SetTransitionToRun() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Animator.speed = 0;
        }
        public override void Update()
        {
            SpeedUp();
            SpeedDown();
        }
        public override void Exit()
        {
            base.Exit();
            Player.Animator.speed = 1;
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