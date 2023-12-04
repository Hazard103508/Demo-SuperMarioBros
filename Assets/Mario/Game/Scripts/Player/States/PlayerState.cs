using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Npc.Koopa;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityShared.Commons.Structs;
using static Mario.Game.ScriptableObjects.Player.PlayerModeProfile;

namespace Mario.Game.Player
{
    public abstract class PlayerState :
        IState,
        IHittableByMovingToTop,
        IHittableByMovingToBottom,
        IHittableByMovingToLeft,
        IHittableByMovingToRight,
        IHittableByKoppa
    {
        #region Objects
        private readonly ISoundService _soundService;
        private readonly IPlayerService _playerService;
        private readonly IGameplayService _gameplayService;

        private bool _jumpWasPressed;
        #endregion

        #region Properties
        protected PlayerController Player { get; private set; }
        #endregion

        #region Constructor
        public PlayerState(PlayerController player)
        {
            _soundService = ServiceLocator.Current.Get<ISoundService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();

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
        public void OnSuperStarActivated()
        {
            Player.Renderer.material = Player.StateMachine.CurrentMode.ModeProfile.StarMaterial;
            _soundService.Play(_playerService.PlayerProfile.Buff.SoundFX);
            _gameplayService.ActivateStarman(() => Player.Renderer.material = Player.StateMachine.CurrentMode.ModeProfile.Default.Material);
        }
        public void OnTimeOut() => SetTransitionToTimeOut();
        public void OnTouchFlag() => SetTransitionToFlag();
        public virtual void OnFall() { }
        public virtual void OnBuff() { }
        public virtual void OnNerf() { }
        public virtual void OnDeath() { }
        public virtual void OnBounceJump() => Player.Movable.SetJumpForce(Player.StateMachine.CurrentMode.ModeProfile.Jump.Bounce);
        public virtual void OnGameUnfrozen() => Player.Movable.enabled = true;
        public virtual void OnGameFrozen() => Player.Movable.enabled = false;
        #endregion

        #region Protected Methods
        protected void SpeedUp()
        {
            if (Player.InputActions.Move != 0 && !Player.InputActions.Ducking)
            {
                float currentAcceleration = Player.InputActions.Sprint ? Player.StateMachine.CurrentMode.ModeProfile.Run.Acceleration : Player.StateMachine.CurrentMode.ModeProfile.Walk.Acceleration;
                Player.Movable.Speed += Player.InputActions.Move * currentAcceleration * Time.deltaTime;

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
        protected virtual void ShootFireball()
        {
            if (Player.InputActions.Fire && Player.StateMachine.CurrentMode.Equals(Player.StateMachine.ModeSuper))
                _playerService.ShootFireball();
        }
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
            if (Player.InputActions.Move != 0 && !Player.InputActions.Ducking)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);

            return false;
        }
        protected virtual bool SetTransitionToStop()
        {
            if (Player.InputActions.Move != 0 && !Player.InputActions.Ducking && Mathf.Sign(Player.Movable.Speed) != Mathf.Sign(Player.InputActions.Move))
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
            if (Player.Movable.JumpForce < -3f)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateFall);

            return false;
        }
        protected virtual bool SetTransitionToDucking()
        {
            if (Player.InputActions.Ducking && Player.InputActions.Move == 0)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateDucking);

            return false;
        }
        protected virtual bool SetTransitionToBuff()
        {
            // SACAR ESTOY DE ACA ----------------------
            Player.StartCoroutine(BuffCO());
            return true;
        }
        protected virtual bool SetTransitionToNerf() => false;//Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateNerf);
        protected virtual bool SetTransitionToDeath()
        {
            if (!Player.IsInvincible)
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateDeath);
                return true;
            }
            return false;
        }
        protected virtual void SetTransitionToDeathFall() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateDeathFall);
        protected virtual bool SetTransitionToTimeOut() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateTimeOut);
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
        protected bool HitObjectOnTop(List<HitObject> hitObjects) => HitObjectOn<IHittableByPlayerFromBottom>(hitObjects, script => script.OnHittedByPlayerFromBottom(Player));
        protected bool HitObjectOnBottom(List<HitObject> hitObjects) => HitObjectOn<IHittableByPlayerFromTop>(hitObjects, script => script.OnHittedByPlayerFromTop(Player));
        protected bool HitObjectOnRight(List<HitObject> hitObjects) => HitObjectOn<IHittableByPlayerFromLeft>(hitObjects, script => script.OnHittedByPlayerFromLeft(Player));
        protected bool HitObjectOnLeft(List<HitObject> hitObjects) => HitObjectOn<IHittableByPlayerFromRight>(hitObjects, script => script.OnHittedByPlayerFromRight(Player));
        #endregion

        #region Private Methods
        private void ChangeMode(PlayerController player)
        {
            Player.Animator.runtimeAnimatorController = Player.StateMachine.CurrentMode.ModeProfile.Default.Animator;
            Player.Renderer.material = Player.StateMachine.CurrentMode.ModeProfile.Default.Material;

            player.Collider.offset = Player.StateMachine.CurrentMode.ModeProfile.Collider.Offset;
            player.Collider.size = Player.StateMachine.CurrentMode.ModeProfile.Collider.Size;
            SetRaycastNormal();
        }
        private void SetRaycast(ModeRaycastRange modeRaycastRange)
        {
            Player.Movable.RaycastTop.Profile = modeRaycastRange.Top;
            Player.Movable.RaycastBottom.Profile = modeRaycastRange.Bottom;
            Player.Movable.RaycastLeft.Profile = modeRaycastRange.Left;
            Player.Movable.RaycastRight.Profile = modeRaycastRange.Right;
        }
        private bool HitObjectOn<T>(List<HitObject> hitObjects, Action<T> onHitFunc)
        {
            foreach (var hit in hitObjects)
            {
                if (hit == null || hit.Object == null)
                    continue;

                if (!hit.Object.TryGetComponent<T>(out var script))
                    continue;

                onHitFunc.Invoke(script);
                return true;
            }
            return false;
        }
        private IEnumerator BuffCO()
        {
            // BORRAR CORRUTINA REDUNDANTE...
            _soundService.Play(_playerService.PlayerProfile.Buff.SoundFX);
            //yield return SetPlayerAnimator(Player.StateMachine.CurrentMode.ModeProfile.Animators.Buff);
            yield return null;

            if (_playerService.IsPlayerSmall())
                ChangeModeToBig(Player);
        }
        //private IEnumerator SetPlayerAnimator(PlayerAnimatorData playerAnimator)
        //{
        //    Player.Renderer.material = playerAnimator.Material;
        //    if (playerAnimator.Animator != null)
        //    {
        //        Player.Animator.runtimeAnimatorController = playerAnimator.Animator;
        //        Player.Animator.Play(GetAnimatorState(), 0, 0);
        //
        //        if (playerAnimator.FreezeTime > 0)
        //        {
        //            _gameplayService.FreezeGame();
        //            yield return new WaitForSeconds(playerAnimator.FreezeTime);
        //            _gameplayService.UnfreezeGame();
        //        }
        //    }
        //}
        #endregion

        #region On Movable Hit
        public virtual void OnHittedByMovingToTop(RayHitInfo hitInfo) { }
        public virtual void OnHittedByMovingToBottom(RayHitInfo hitInfo) { }
        public virtual void OnHittedByMovingToLeft(RayHitInfo hitInfo) { }
        public virtual void OnHittedByMovingToRight(RayHitInfo hitInfo) { }
        #endregion

        #region On Koopa Hit
        public virtual void OnHittedByKoppa(Koopa koopa) => Player.Hit(koopa);
        #endregion
    }
}
