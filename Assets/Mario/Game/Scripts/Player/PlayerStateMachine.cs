using Mario.Game.Commons;

namespace Mario.Game.Player
{
    public class PlayerStateMachine : StateMachine
    {
        #region Properties
        new public PlayerState CurrentState => (PlayerState)base.CurrentState;
        public PlayerStateSmallIdle StateSmallIdle { get; private set; }
        //public GoombaStateWalk StateWalk { get; private set; }
        //public GoombaStateDead StateDead { get; private set; }
        #endregion

        #region Constructor
        public PlayerStateMachine(PlayerController Player)
        {
            StateSmallIdle = new PlayerStateSmallIdle(Player);
            //this.StateWalk = new GoombaStateWalk(goomba);
            //this.StateHit = new GoombaStateHit(goomba);
            //this.StateDead = new GoombaStateDead(goomba);
        }
        #endregion
    }
}