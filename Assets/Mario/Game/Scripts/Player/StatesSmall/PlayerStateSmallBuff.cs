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
        private void ChangePlayerMode()
        {
            Player.StateMachine.ChangeModeToBig(Player);
            Player.Movable.enabled = true;
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
            if (_timer >= 0.5f)
            {
                ChangePlayerMode();
                SetNextState();
            }

            _timer += Time.deltaTime;
        }
        #endregion
    }
}