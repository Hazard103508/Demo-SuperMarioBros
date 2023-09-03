namespace Mario.Game.Player
{
    public abstract class PlayerStateBuff : PlayerState
    {
        #region Constructor
        public PlayerStateBuff(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Buff";
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Movable.enabled = false;
        }
        #endregion
    }
}