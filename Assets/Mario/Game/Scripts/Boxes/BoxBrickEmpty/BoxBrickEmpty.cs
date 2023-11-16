using Mario.Game.ScriptableObjects.Boxes;

namespace Mario.Game.Boxes.BrickBoxEmpty
{
    public class BoxBrickEmpty : Box.Box
    {
        #region Properties
        new public BrickBoxEmptyProfile Profile => (BrickBoxEmptyProfile)base.Profile;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateJump = new BoxBrickEmptyStateJump(this);
        }
        #endregion
    }
}