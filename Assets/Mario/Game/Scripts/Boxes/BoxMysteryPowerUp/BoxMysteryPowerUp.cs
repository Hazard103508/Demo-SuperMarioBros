using Mario.Game.ScriptableObjects.Boxes;

namespace Mario.Game.Boxes.MysteryBoxPowerUp
{
    public class BoxMysteryPowerUp : Box.Box
    {
        #region Properties
        new public MysteryBoxPowerUpProfile Profile => (MysteryBoxPowerUpProfile)base.Profile;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateIdle = new BoxMysteryPowerUpStateIdle(this);
        }
        #endregion
    }
}