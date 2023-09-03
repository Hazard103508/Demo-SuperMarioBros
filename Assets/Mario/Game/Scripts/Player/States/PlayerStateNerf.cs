namespace Mario.Game.Player
{
    public abstract class PlayerStateNerf : PlayerState
    {
        #region Constructor
        public PlayerStateNerf(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Nerf";
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