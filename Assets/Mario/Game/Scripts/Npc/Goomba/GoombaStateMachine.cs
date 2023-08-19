using Mario.Game.Commons;

namespace Mario.Game.Npc.Goomba
{
    public class GoombaStateMachine : StateMachine
    {
        #region Properties
        new public GoombaState CurrentState => (GoombaState)base.CurrentState;
        public GoombaStateWalk StateWalk { get; private set; }
        public GoombaStateHit StateHit { get; private set; }
        public GoombaStateDead StateDead { get; private set; }
        #endregion

        #region Constructor
        public GoombaStateMachine(Goomba goomba)
        {
            this.StateWalk = new GoombaStateWalk(goomba);
            this.StateHit = new GoombaStateHit(goomba);
            this.StateDead = new GoombaStateDead(goomba);
        }
        #endregion
    }
}