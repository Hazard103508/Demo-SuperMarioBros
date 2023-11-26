using Mario.Game.ScriptableObjects.Boxes;

namespace Mario.Game.Boxes.BoxBrickStar
{
    public class BoxBrickStar : Box.Box
    {
        #region Properties
        new public BrickBoxStarProfile Profile => (BrickBoxStarProfile)base.Profile;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateLastJump = new BoxBrickStarStateLastJump(this);
            base.IsLastJump = true;
        }
        #endregion
    }
}