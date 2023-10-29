using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes.BrickBoxCoin
{
    public class BoxBrickCoin : Box.Box
    {
        #region Objects
        private float _limitTime;
        #endregion

        #region Properties
        new public BrickBoxCoinProfile Profile => (BrickBoxCoinProfile)base.Profile;
        public bool IsTimerRunning { get; set; }
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateIdle = new BoxBrickCoinStateIdle(this);
            _limitTime = Profile.LimitTime;
        }
        protected override void Update()
        {
            if (IsTimerRunning)
            {
                _limitTime = Mathf.Max(_limitTime - Time.deltaTime, 0);
                if (_limitTime == 0)
                    IsLastJump = true;
            }

            base.Update();
        }
        #endregion
    }
}