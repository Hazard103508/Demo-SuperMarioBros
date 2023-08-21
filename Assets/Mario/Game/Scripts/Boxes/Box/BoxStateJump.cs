using Mario.Game.Boxes.Box;

namespace Mario.Game.Boxes.Box
{
    public class BoxStateJump : BoxState
    {
        #region Constructor
        public BoxStateJump(Box box) : base(box)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Box.Animator.SetTrigger("Jump");
            // completar....
        }
        #endregion
    }
}