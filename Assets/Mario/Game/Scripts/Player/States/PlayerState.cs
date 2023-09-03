using Mario.Game.Interfaces;
using System.Collections;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public abstract class PlayerState :
        IState,
        IHittableByMovingToTop,
        IHittableByMovingToBottom,
        IHittableByMovingToLeft,
        IHittableByMovingToRight
    {

        #region Objects
        private bool _jumpWasPressed;
        #endregion

        #region Properties
        protected PlayerController Player { get; private set; }
        #endregion


        #region Constructor
        public PlayerState(PlayerController player)
        {
            Player = player;
        }
        #endregion

        #region State Machine
        public virtual void Enter()
        {
            string newAnimatorState = GetAnimatorState();
            if (!string.IsNullOrWhiteSpace(newAnimatorState))
                Player.Animator.CrossFade(newAnimatorState, 0);

            _jumpWasPressed = Player.InputActions.Jump;
        }
        public virtual void Exit()
        {
        }
        public virtual void Update()
        {
        }
        #endregion

        #region Public Methods
        public virtual void OnBuff() { }
        public virtual void OnNerf() { }
        public virtual void OnDeath() { }
        public virtual void OnTouchFlag() { }
        #endregion

        #region Protected Methods
        protected void SpeedUp()
        {
            if (Player.InputActions.Move.x != 0 && !Player.InputActions.Ducking)
            {
                float currentAcceleration = Player.InputActions.Sprint ? Player.StateMachine.CurrentMode.ModeProfile.Run.Acceleration : Player.StateMachine.CurrentMode.ModeProfile.Walk.Acceleration;
                Player.Movable.Speed += Player.InputActions.Move.x * currentAcceleration * Time.deltaTime;

                float _speed = Player.InputActions.Sprint ? Player.StateMachine.CurrentMode.ModeProfile.Run.MaxSpeed : Player.StateMachine.CurrentMode.ModeProfile.Walk.MaxSpeed;
                Player.Movable.Speed = Mathf.Clamp(Player.Movable.Speed, -_speed, _speed);
            }
        }
        protected void SpeedDown()
        {
            if (Player.Movable.Speed != 0)
            {
                float currentDeacceleration = Player.InputActions.Sprint ? Player.StateMachine.CurrentMode.ModeProfile.Run.Deacceleration : Player.StateMachine.CurrentMode.ModeProfile.Walk.Deacceleration;
                Player.Movable.Speed = Mathf.MoveTowards(Player.Movable.Speed, 0, currentDeacceleration * Time.deltaTime);
            }
        }
        protected void SetAnimationSpeed()
        {
            float walkSpeedFactor = Mathf.Abs(Player.Movable.Speed) / Player.StateMachine.CurrentMode.ModeProfile.Walk.MaxSpeed;
            Player.Animator.speed = Mathf.Clamp(walkSpeedFactor, 0.5f, 1.5f);
        }
        protected void ResetAnimationSpeed() => Player.Animator.speed = 1;
        protected void SetRaycastDucking() => SetRaycast(Player.StateMachine.CurrentMode.ModeProfile.DuckingRaycastRange);
        protected void SetRaycastNormal() => SetRaycast(Player.StateMachine.CurrentMode.ModeProfile.NormalRaycastRange);

        protected virtual string GetAnimatorState() => string.Empty;
        protected virtual void SetSpriteDirection() => Player.Renderer.flipX = Player.Movable.Speed < 0;
        protected virtual bool SetTransitionToIdle()
        {
            if (Player.Movable.Speed == 0)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateIdle);

            return false;
        }
        protected virtual bool SetTransitionToRun()
        {
            if (Player.InputActions.Move.x != 0 && !Player.InputActions.Ducking)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);

            return false;
        }
        protected virtual bool SetTransitionToStop()
        {
            if (Player.InputActions.Move.x != 0 && !Player.InputActions.Ducking && Mathf.Sign(Player.Movable.Speed) != Mathf.Sign(Player.InputActions.Move.x))
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateStop);

            return false;
        }
        protected virtual bool SetTransitionToJump()
        {
            if (!_jumpWasPressed && Player.InputActions.Jump)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateJump);

            _jumpWasPressed = Player.InputActions.Jump;
            return false;
        }
        protected virtual bool SetTransitionToDuckingJump()
        {
            if (!_jumpWasPressed && Player.InputActions.Jump)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateDuckingJump);

            _jumpWasPressed = Player.InputActions.Jump;
            return false;
        }
        protected virtual bool SetTransitionToFall()
        {
            if (Player.Movable.JumpForce < 0)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateFall);

            return false;
        }
        protected virtual bool SetTransitionToDucking()
        {
            if (Player.InputActions.Ducking && Player.InputActions.Move.x == 0)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateDucking);

            return false;
        }
        protected virtual bool SetTransitionToBuff() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateBuff);
        protected virtual bool SetTransitionToNerf() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateNerf);
        protected virtual bool SetTransitionToDeath() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateDeath);
        protected virtual bool SetTransitionToFlag() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateFlag);

        protected void ChangeModeToSmall(PlayerController player)
        {
            Player.StateMachine.CurrentMode = Player.StateMachine.ModeSmall;
            ChangeMode(player);
        }
        protected void ChangeModeToBig(PlayerController player)
        {
            Player.StateMachine.CurrentMode = Player.StateMachine.ModeBig;
            ChangeMode(player);
        }
        protected void ChangeModeToSuper(PlayerController player)
        {
            Player.StateMachine.CurrentMode = Player.StateMachine.ModeSuper;
            ChangeMode(player);
        }
        #endregion

        #region Private Methods
        private void ChangeMode(PlayerController player)
        {
            player.Animator.runtimeAnimatorController = Player.StateMachine.CurrentMode.ModeProfile.AnimatorController;
            SetRaycastNormal();
        }
        private void SetRaycast(ScriptableObjects.Player.PlayerModeProfile.ModeRaycastRange modeRaycastRange)
        {
            Player.Movable.RaycastTop.Profile = modeRaycastRange.Top;
            Player.Movable.RaycastBottom.Profile = modeRaycastRange.Bottom;
            Player.Movable.RaycastLeft.Profile = modeRaycastRange.Left;
            Player.Movable.RaycastRight.Profile = modeRaycastRange.Right;
        }
        #endregion

        #region On Movable Hit
        public virtual void OnHittedByMovingToTop(RayHitInfo hitInfo) { }
        public virtual void OnHittedByMovingToBottom(RayHitInfo hitInfo) { }
        public virtual void OnHittedByMovingToLeft(RayHitInfo hitInfo) { }
        public virtual void OnHittedByMovingToRight(RayHitInfo hitInfo) { }
        #endregion
    }
}
