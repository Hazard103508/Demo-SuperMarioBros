namespace Mario.Game.Player
{
    public class PlayerStateDuckingFall : PlayerStateFall
    {
        #region Constructor
        public PlayerStateDuckingFall(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Ducking";
        protected override void ShootFireball()
        {
        }
        #endregion
    }
}