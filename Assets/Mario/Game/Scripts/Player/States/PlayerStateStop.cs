using Unity.VisualScripting;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateStop : PlayerState
    {
        #region Objects
        private bool _jumpWasPressed;
        #endregion

        #region Constructor
        public PlayerStateStop(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Public Methods
        public override void OnBuff() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateBuff);
        #endregion

        #region protected Methods
        protected override void SetSpriteDirection() => Player.Renderer.flipX = Player.Movable.Speed > 0;
        protected override bool SetTransitionToRun()
        {
            if (Mathf.Sign(Player.Movable.Speed) == Mathf.Sign(Player.InputActions.Move.x))
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);
                return true;
            }
            return false;
        }

        #endregion

        #region Private Methods
        private bool SetTransitionToJump()
        {
            if (!_jumpWasPressed && Player.InputActions.Jump)
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateJump);
                return true;
            }

            _jumpWasPressed = Player.InputActions.Jump;
            return false;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Stop", 0);
            _jumpWasPressed = Player.InputActions.Jump;
        }
        public override void Update()
        {
            SpeedUp();
            SpeedDown();

            if (SetTransitionToIdle())
                return;

            SetSpriteDirection();

            if (SetTransitionToJump())
                return;

            SetTransitionToRun();
        }
        #endregion

        #region On Movable Hit
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