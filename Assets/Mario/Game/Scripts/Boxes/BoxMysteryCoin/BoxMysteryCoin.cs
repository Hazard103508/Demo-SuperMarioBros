using Mario.Game.ScriptableObjects.Boxes;

namespace Mario.Game.Boxes.MysteryBoxCoin
{
    public class BoxMysteryCoin : Box.Box
    {
        #region Properties
        new public MysteryBoxCoinProfile Profile => (MysteryBoxCoinProfile)base.Profile;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateIdle = new BoxMysteryCoinStateIdle(this);
        }
        #endregion
    }
}