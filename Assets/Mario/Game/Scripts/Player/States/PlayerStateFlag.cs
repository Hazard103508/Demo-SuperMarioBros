namespace Mario.Game.Player
{
    public class PlayerStateFlag : PlayerState
    {
        #region Constructor
        public PlayerStateFlag(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Animator.CrossFade("Flag", 0);
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