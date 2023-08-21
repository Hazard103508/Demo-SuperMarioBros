using Mario.Game.Boxes.Box;

namespace Mario.Game.Boxes.InvisibleBox1UP
{
    public class InvisibleBox1UPStateIdle : BoxStateIdle
    {
        #region Constructor
        public InvisibleBox1UPStateIdle(Box.Box box) : base(box)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Box.IsLastJump = true;
        }
        #endregion
    }
}