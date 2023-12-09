using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Commons.Helpers;
using Mario.Commons.Structs;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateJump : PlayerState
    {
        #region Objects
        private readonly ISoundService _soundService;

        private float _jumpForce;
        private float _initYPos;
        private float _maxHeight;
        #endregion

        #region Constructor
        public PlayerStateJump(PlayerController player) : base(player)
        {
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region Public Methods
        public override void OnFall() => SetTransitionToDeathFall();
        public override void OnDeath() => SetTransitionToDeath();
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Jump";
        protected override bool SetTransitionToFall()
        {
            if (!Player.InputActions.Jump || Player.Movable.JumpForce <= 0)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateFall);

            return false;
        }
        #endregion

        #region Private Methods
        private void Jump()
        {
            var jumpHeight = MathEquations.Trajectory.GetHeight(_jumpForce, -Player.Movable.Gravity);
            var jumpedHeight = Player.transform.position.y - _initYPos;
            var currentJump = jumpHeight + jumpedHeight;
            if (currentJump < _maxHeight)
                Player.Movable.SetJumpForce(_jumpForce);
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
            _initYPos = Player.transform.position.y;
            _maxHeight = GetMaxHeight();

            float minHeight = Player.InputActions.Move == 0 ? Player.StateMachine.CurrentMode.ModeProfile.Jump.WalkHeight.Min : Player.StateMachine.CurrentMode.ModeProfile.Jump.RunHeight.Min;
            _jumpForce = MathEquations.Trajectory.GetVelocity(minHeight, -Player.Movable.Gravity);

            if (Player.Movable.JumpForce == 0)
                _soundService.Play(Player.StateMachine.CurrentMode.ModeProfile.Jump.SoundFX, Player.transform.position);

            Player.Movable.SetJumpForce(_jumpForce);
        }
        public override void Update()
        {
            SpeedUp();
            SpeedDown();
            Jump();
            SetTransitionToFall();
            ShootFireball();
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToTop(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
            {
                Player.Movable.SetJumpForce(0);
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateFall);
            }
            HitObjectOnTop(hitInfo.hitObjects);
        }
        public override void OnHittedByMovingToBottom(RayHitInfo hitInfo) => HitObjectOnBottom(hitInfo.hitObjects);
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