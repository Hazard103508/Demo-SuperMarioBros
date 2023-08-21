using Mario.Game.Items.Mushroom;
using Mario.Game.ScriptableObjects.Items;

namespace Mario.Game.Items.RedMushroom
{
    public class MushroomRed : Mushroom.Mushroom
    {
        #region Properties
        new public RedMushroomProfile Profile => (RedMushroomProfile)base.Profile;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateWalk = new MushroomRedStateWalk(this);
        }
        #endregion
    }
}