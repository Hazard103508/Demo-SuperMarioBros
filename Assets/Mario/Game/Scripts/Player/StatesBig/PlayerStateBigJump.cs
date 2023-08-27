namespace Mario.Game.Player
{
    public class PlayerStateBigJump : PlayerStateJump
    {
        #region Constructor
        public PlayerStateBigJump(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Animator.CrossFade("Big_Jump", 0);
        }
        #endregion
    }
}