using Mario.Game.ScriptableObjects.Boxes;

namespace Mario.Game.Boxes.BrickBoxCoin
{
    public class BrickBoxCoin : Box.Box
    {
        #region Properties
        new public BrickBoxCoinProfile Profile => (BrickBoxCoinProfile)base.Profile;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateIdle = new BrickBoxCoinStateIdle(this);
        }
        #endregion
    }
}