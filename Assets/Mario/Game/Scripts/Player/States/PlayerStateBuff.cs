namespace Mario.Game.Player
{
    public abstract class PlayerStateBuff : PlayerState
    {
        #region Constructor
        public PlayerStateBuff(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Buff", 0);
        }
        #endregion
    }
}