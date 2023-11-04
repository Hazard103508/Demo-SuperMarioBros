using Mario.Game.Boxes.Box;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Boxes.InvisibleBox1UP
{
    public class BoxInvisible1UpStateIdle : BoxStateIdle
    {
        #region Objects
        private float _delay;
        #endregion

        #region Constructor
        public BoxInvisible1UpStateIdle(Box.Box box) : base(box)
        {
        }
        #endregion

        #region Private Methods
        private void StartDelay() => _delay = 0.4f;
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            _delay = 0;
        }
        public override void Update()
        {
            base.Update();
            _delay = Mathf.Max(_delay - Time.deltaTime, 0);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player) => StartDelay();
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            if (_delay == 0)
            {
                Box.IsLastJump = true;
                base.OnHittedByPlayerFromBottom(player);
            }
        }
        public override void OnHittedByPlayerFromLeft(PlayerController player) => StartDelay();
        public override void OnHittedByPlayerFromRight(PlayerController player) => StartDelay();
        #endregion
    }
}