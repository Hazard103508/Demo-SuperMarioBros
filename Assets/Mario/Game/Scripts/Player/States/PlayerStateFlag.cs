namespace Mario.Game.Player
{
    public class PlayerStateFlag : PlayerState
    {
        #region Constructor
        public PlayerStateFlag(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Flag";
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Movable.enabled = false;
        }
        public override void Exit()
        {
            base.Exit();
            Player.Movable.enabled = true;
        }
        #endregion
    }
}