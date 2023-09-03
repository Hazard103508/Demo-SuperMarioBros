namespace Mario.Game.Player
{
    public class PlayerStateDuckingJump : PlayerStateJump
    {
        #region Constructor
        public PlayerStateDuckingJump(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override void SetAnimatorState(string stateName) => base.SetAnimatorState("Ducking");
        #endregion
    }
}