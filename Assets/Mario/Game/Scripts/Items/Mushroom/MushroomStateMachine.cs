using Mario.Game.Commons;

namespace Mario.Game.Items.Mushroom
{ 
    public class MushroomStateMachine : StateMachine
    {
        #region Properties
        new public MushroomState CurrentState => (MushroomState)base.CurrentState;
        public MushroomStateWalk StateWalk { get; private set; }
        #endregion

        #region Constructor
        public MushroomStateMachine(Mushroom mushroom)
        {
            this.StateWalk = new MushroomStateWalk(mushroom);
        }
        #endregion
    }
}