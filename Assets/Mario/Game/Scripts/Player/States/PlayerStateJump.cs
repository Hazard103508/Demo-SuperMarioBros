using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateJump : PlayerState
    {
        #region Objects
        private float _jumpForce;
        private float _initYPos;
        private float _maxHeight;
        #endregion

        #region Constructor
        public PlayerStateJump(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Public Methods
        public override void OnBuff() => SetTransitionToBuff();
        public override void OnNerf() => SetTransitionToNerf();
        public override void OnDeath() => SetTransitionToDeath();
        public override void OnTouchFlag() => SetTransitionToFlag();
        #endregion

        #region Protected
        protected override bool SetTransitionToFall()
        {
            if (!Player.InputActions.Jump || Player.Movable.JumpForce < 0)
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateFall);
                return true;
            }

            return false;
        }
        #endregion

        #region Private Methods
        private void Jump()
        {
            var jumpHeight = UnityShared.Helpers.MathEquations.Trajectory.GetHeight(_jumpForce, -Player.Movable.Gravity);
            var jumpedHeight = Player.transform.position.y - _initYPos;
            var currentJump = jumpHeight + jumpedHeight;
            if (currentJump < _maxHeight)
                Player.Movable.AddJumpForce(_jumpForce);
        }
        private float GetMaxHeight()
        {
            float absCurrentSpeed = Mathf.Abs(Player.Movable.Speed);
            float speedFactor = Mathf.InverseLerp(Player.StateMachine.CurrentMode.ModeProfile.Walk.MaxSpeed, Player.StateMachine.CurrentMode.ModeProfile.Run.MaxSpeed, absCurrentSpeed);
            return Mathf.Lerp(Player.StateMachine.CurrentMode.ModeProfile.Jump.WalkHeight.Max, Player.StateMachine.CurrentMode.ModeProfile.Jump.RunHeight.Max, speedFactor);
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Animator.CrossFade("Jump", 0);
            _initYPos = Player.transform.position.y;
            _maxHeight = GetMaxHeight();

            float minHeight = Player.InputActions.Move.x == 0 ? Player.StateMachine.CurrentMode.ModeProfile.Jump.WalkHeight.Min : Player.StateMachine.CurrentMode.ModeProfile.Jump.RunHeight.Min;
            _jumpForce = UnityShared.Helpers.MathEquations.Trajectory.GetVelocity(minHeight, -Player.Movable.Gravity);
            Player.Movable.AddJumpForce(_jumpForce);
        }
        public override void Update()
        {
            SpeedUp();
            Jump();
            SetTransitionToFall();
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToTop(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
            {
                Player.Movable.AddJumpForce(0);
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateFall);
            }
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