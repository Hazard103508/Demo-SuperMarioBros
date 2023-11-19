using Mario.Game.Commons;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateMachine : StateMachine
    {
        #region Properties
        new public KoopaState CurrentState => (KoopaState)base.CurrentState;
        public KoopaStateWalk StateWalk { get; set; }
        public KoopaStateInShell StateInShell { get; private set; }
        public KoopaStateWakingUp StateWakingUp { get; private set; }
        public KoopaStateBouncing StateBouncing { get; private set; }
        public KoopaStateDead StateDead { get; private set; }
        #endregion

        #region Constructor
        public KoopaStateMachine(Koopa koopa)
        {
            this.StateWalk = new KoopaStateWalk(koopa);
            this.StateInShell = new KoopaStateInShell(koopa);
            this.StateWakingUp = new KoopaStateWakingUp(koopa);
            this.StateBouncing = new KoopaStateBouncing(koopa);
            this.StateDead = new KoopaStateDead(koopa);
        }
        #endregion
    }
}