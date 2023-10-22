using Mario.Game.Commons;

namespace Mario.Game.Items.Mushroom
{
    public class MushroomStateMachine : StateMachine
    {
        #region Properties
        new public MushroomState CurrentState => (MushroomState)base.CurrentState;
        public MushroomStateRising StateRising { get; set; }
        public MushroomStateWalk StateWalk { get; set; }
        public MushroomStateJump StateJump { get; private set; }
        #endregion

        #region Constructor
        public MushroomStateMachine(Mushroom mushroom)
        {
            this.StateRising = new MushroomStateRising(mushroom);
            this.StateWalk = new MushroomStateWalk(mushroom);
            this.StateJump = new MushroomStateJump(mushroom);
        }
        #endregion
    }
}