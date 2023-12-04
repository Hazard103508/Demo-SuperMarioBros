using Mario.Game.Commons;

namespace Mario.Game.Npc.Plant
{
    public class PlantStateMachine : StateMachine
    {
        #region Properties
        new public PlantState CurrentState => (PlantState)base.CurrentState;
        public PlantStateIdle StateIdle { get; private set; }
        public PlantStateRising StateRising { get; private set; }
        public PlantStateHiding StateHiding { get; private set; }
        public PlantStateHiden StateHiden { get; private set; }

        #endregion

        #region Constructor
        public PlantStateMachine(Plant plant)
        {
            StateRising = new PlantStateRising(plant);
            StateIdle = new PlantStateIdle(plant);
            StateHiding = new PlantStateHiding(plant);
            StateHiden = new PlantStateHiden(plant);
        }
        #endregion
    }
}