using Mario.Game.Commons;

namespace Mario.Game.Items.Star
{
    public class StarStateMachine : StateMachine
    {
        #region Properties
        new public StarState CurrentState => (StarState)base.CurrentState;
        public StarStateRising StateRising { get; set; }
        public StarStateJumping StateJumping { get; private set; }
        #endregion

        #region Constructor
        public StarStateMachine(Star star)
        {
            this.StateRising = new StarStateRising(star);
            this.StateJumping = new StarStateJumping(star);
        }
        #endregion
    }
}