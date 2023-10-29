using Mario.Game.ScriptableObjects.Boxes;

namespace Mario.Game.Boxes.InvisibleBox1UP
{
    public class BoxInvisible1Up : Box.Box
    {
        #region Properties
        new public InvisibleBox1UPProfile Profile => (InvisibleBox1UPProfile)base.Profile;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateIdle = new BoxInvisible1UpStateIdle(this);
            base.StateMachine.StateLastJump = new BoxInvisible1UpStateLastJump(this);
        }
        #endregion
    }
}