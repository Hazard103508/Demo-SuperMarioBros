namespace Mario.Game.Boxes.Box
{
    public class BoxStateDisable : BoxState
    {
        #region Constructor
        public BoxStateDisable(Box box) : base(box)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Box.Animator.SetTrigger("Disable");
            Box.Movable.RemoveGravity();
        }
        #endregion
    }
}