namespace Mario.Game.Player
{
    public abstract class PlayerStateNerf : PlayerState
    {
        #region Constructor
        public PlayerStateNerf(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Movable.enabled = false;
            Player.Animator.CrossFade("Nerf", 0);
        }
        #endregion
    }
}