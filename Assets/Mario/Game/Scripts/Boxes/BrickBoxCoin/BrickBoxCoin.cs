using Mario.Game.ScriptableObjects.Boxes;

namespace Mario.Game.Boxes.BrickBoxCoin
{
    public class BrickBoxCoin : Box.Box
    {
        #region Objects
        //private float _limitTime;
        //private bool _started;
        #endregion

        #region Properties
        new public BrickBoxCoinProfile Profile => (BrickBoxCoinProfile)base.Profile;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateIdle = new BrickBoxCoinStateIdle(this);
        }
        //private void Update()
        //{
        //    if (_started)
        //    {
        //        _limitTime -= Time.deltaTime;
        //        if (_limitTime < 0)
        //            IsLastJump = true;
        //    }
        //}
        #endregion
    }
}