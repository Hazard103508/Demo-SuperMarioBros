using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateBuffFlower : PlayerStateBuff
    {
        #region Objects
        private float _timer;
        #endregion

        #region Constructor
        public PlayerStateBuffFlower(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override bool SetTransitionToIdle() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateIdle);
        protected override bool SetTransitionToRun()
        {
            if (Player.Movable.Speed != 0)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);

            return false;
        }
        protected override bool SetTransitionToJump()
        {
            if (Player.Movable.JumpForce > 0)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateJump);

            return false;
        }
        protected override bool SetTransitionToStop()
        {
            if ((Player.Movable.Speed < 0 && !Player.Renderer.flipX) || Player.Movable.Speed > 0 && Player.Renderer.flipX)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateStop);

            return false;
        }
        protected override bool SetTransitionToFall()
        {
            if (base.SetTransitionToFall())
            {
                Player.Animator.CrossFade("Jump", 0);
                return true;
            }
            return false;
        }
        protected override bool SetTransitionToDucking()
        {
            if (Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Ducking)
                return Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateDucking);
            return false;
        }
        #endregion

        #region Private Methods
        private void ChangePlayerMode()
        {
            Player.Movable.enabled = true;
            base.ChangeModeToSuper(Player);
        }
        private void SetNextState()
        {
            if (SetTransitionToDucking())
                return;

            if (SetTransitionToJump())
                return;

            if (SetTransitionToFall())
                return;

            if (SetTransitionToStop())
                return;

            if (SetTransitionToRun())
                return;

            SetTransitionToIdle();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            _timer = 0;
        }
        public override void Update()
        {
            base.Update();
            if (_timer >= 1f)
            {
                ChangePlayerMode();
                SetNextState();
            }

            _timer += Time.deltaTime;
        }
        #endregion
    }
}