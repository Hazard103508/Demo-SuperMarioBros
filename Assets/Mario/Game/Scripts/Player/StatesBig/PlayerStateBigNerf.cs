using System.Collections;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateBigNerf : PlayerStateNerf
    {
        #region Objects
        private float _timer;
        #endregion

        #region Constructor
        public PlayerStateBigNerf(PlayerController player) : base(player)
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
        #endregion

        #region Private Methods
        private void ChangePlayerMode()
        {
            Player.StartCoroutine(SetInvincible());
            Player.Movable.enabled = true;
            base.ChangeModeToSmall(Player);
        }
        private void SetNextState()
        {
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
        private IEnumerator SetInvincible()
        {
            Player.IsInvincible = true;
            yield return new WaitForEndOfFrame();

            float _intervalTime = 0.05f;
            float _intervalCount = 2.5f / _intervalTime;

            var _colorEnabled = Color.white;
            var _colorDisable = new Color(0, 0, 0, 0);
            for (int i = 0; i < _intervalCount; i++)
            {
                Player.Renderer.color = i % 2 == 0 ? _colorEnabled : _colorDisable;
                yield return new WaitForSeconds(_intervalTime);
            }

            Player.Renderer.color = _colorEnabled;
            Player.IsInvincible = false;
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