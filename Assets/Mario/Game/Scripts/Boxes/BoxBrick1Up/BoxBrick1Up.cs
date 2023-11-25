using Mario.Game.ScriptableObjects.Boxes;

namespace Mario.Game.Boxes.BoxBrick1Up
{
    public class BoxBrick1Up : Box.Box
    {
        #region Properties
        new public BrickBox1UPProfile Profile => (BrickBox1UPProfile)base.Profile;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateLastJump = new BoxBrick1UpStateLastJump(this);
            base.IsLastJump = true;
        }
        #endregion
    }
}