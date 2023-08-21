using Mario.Game.Commons;

namespace Mario.Game.Boxes.Box
{
    public class BoxStateMachine : StateMachine
    {
        #region Properties
        new public BoxState CurrentState => (BoxState)base.CurrentState;
        public BoxStateIdle StateIdle { get; set; }
        public BoxStateJump StateJump { get; set; }
        public BoxStateLastJump StateLastJump { get; set; }
        public BoxStateDisable StateDisable { get; set; }
        #endregion

        #region Constructor
        public BoxStateMachine(Box box)
        {
            this.StateIdle = new BoxStateIdle(box);
            this.StateJump = new BoxStateJump(box);
            this.StateLastJump = new BoxStateLastJump(box);
            this.StateDisable = new BoxStateDisable(box);
        }
        #endregion
    }
}