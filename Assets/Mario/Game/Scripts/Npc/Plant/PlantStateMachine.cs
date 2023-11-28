using Mario.Game.Commons;

namespace Mario.Game.Npc.Plant
{
    public class PlantStateMachine : StateMachine
    {
        #region Properties
        new public PlantState CurrentState => (PlantState)base.CurrentState;
        public PlantStateIdle StateIdle { get; private set; }
        //public GoombaStateHit StateHit { get; private set; }
        //public GoombaStateDead StateDead { get; private set; }
        #endregion

        #region Constructor
        public PlantStateMachine(Plant plant)
        {
            StateIdle = new PlantStateIdle(plant);
            //this.StateWalk = new GoombaStateWalk(goomba);
            //this.StateHit = new GoombaStateHit(goomba);
            //this.StateDead = new GoombaStateDead(goomba);
        }
        #endregion
    }
}