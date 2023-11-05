using Mario.Game.ScriptableObjects.Boxes;

namespace Mario.Game.Boxes.MysteryBoxPowerUp
{
    public class BoxBrickPowerUp : Box.Box
    {
        #region Properties
        new public BoxPowerUpProfile Profile => (BoxPowerUpProfile)base.Profile;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateIdle = new BoxBrickPowerUpStateIdle(this);
        }
        #endregion
    }
}