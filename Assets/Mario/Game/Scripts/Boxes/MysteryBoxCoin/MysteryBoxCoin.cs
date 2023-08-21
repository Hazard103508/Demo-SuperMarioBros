using Mario.Game.ScriptableObjects.Boxes;

namespace Mario.Game.Boxes.MysteryBoxCoin
{
    public class MysteryBoxCoin : Box.Box
    {
        #region Properties
        new public MysteryBoxCoinProfile Profile => (MysteryBoxCoinProfile)base.Profile;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateIdle = new MysteryBoxCoinStateIdle(this);
        }
        #endregion
    }
}