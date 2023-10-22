using Mario.Game.Commons;

namespace Mario.Game.Items.Flower
{
    public class FlowerStateMachine : StateMachine
    {
        #region Properties
        new public FlowerState CurrentState => (FlowerState)base.CurrentState;
        public FlowerStateRising StateRising { get; private set; }
        public FlowerStateIdle StateIdle { get; set; }
        #endregion

        #region Constructor
        public FlowerStateMachine(Flower flower)
        {
            this.StateRising = new FlowerStateRising(flower);
            this.StateIdle = new FlowerStateIdle(flower);
        }
        #endregion
    }
}