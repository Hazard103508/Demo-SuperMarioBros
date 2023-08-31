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
        public override void OnTouchFlag() => SetTransitionToFlag();
        #endregion

        #region Protected Methods
        protected override bool SetTransitionToRun()
        {
            Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);
            return true;
        }
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
        }
        public override void Exit()
        {
            base.Exit();
            Player.Animator.speed = 1;
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToTop(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.AddJumpForce(0);
        }
        public override void OnHittedByMovingToBottom(RayHitInfo hitInfo)
        {
            if (SetTransitionToIdle())
                return;

            SetTransitionToRun();
        }
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.Speed = 0;
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.Speed = 0;
        }
        #endregion
    }
}