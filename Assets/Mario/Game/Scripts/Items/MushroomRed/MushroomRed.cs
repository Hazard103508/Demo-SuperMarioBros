using Mario.Game.ScriptableObjects.Items;

namespace Mario.Game.Items.RedMushroom
{
    public class MushroomRed : Mushroom.Mushroom
    {
        #region Properties
        new public MushroomRedProfile Profile => (MushroomRedProfile)base.Profile;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateWalk = new MushroomRedStateWalk(this);
            base.StateMachine.StateRising = new MushroomRedStateRising(this);
        }
        #endregion
    }
}