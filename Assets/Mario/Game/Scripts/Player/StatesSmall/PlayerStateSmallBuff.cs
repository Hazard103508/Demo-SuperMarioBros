using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateSmallBuff : PlayerStateBuff
    {
        #region Objects
        private float _timer;
        #endregion

        #region Constructor
        public PlayerStateSmallBuff(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Private Methods
        private void SetTransitionToNextState()
        {
            Player.StateMachine.ChangeModeToBig(Player);
            Player.Movable.enabled = true;

            if (Player.Movable.Speed == 0)
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);
            else
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateIdle);
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Movable.enabled = false;
            _timer = 0;
        }
        public override void Update()
        {
            base.Update();
            if (_timer >= 0.5f)
                SetTransitionToNextState();

            _timer += Time.deltaTime;
        }
        #endregion
    }
}