using Mario.Game.Commons;

namespace Mario.Game.Player
{
    public class PlayerStateMachine : StateMachine
    {
        #region Properties
        public PlayerMode CurrentMode { get; set; }
        public PlayerMode ModeSmall { get; private set; }
        public PlayerMode ModeBig { get; private set; }
        public PlayerMode ModeSuper { get; private set; }

        new public PlayerState CurrentState => (PlayerState)base.CurrentState;
        #endregion

        #region Constructor
        public PlayerStateMachine(PlayerController Player)
        {
            ModeSmall = new PlayerModeSmall(Player);
            ModeBig = new PlayerModeBig(Player);
            //ModeSuper = new PlayerModeSuper(Player);

            CurrentMode = ModeSmall;
        }
        #endregion

        #region Public Methods
        public void ChangeModeToSmall(PlayerController player)
        {
            ChangeMode(player, player.Profile.Modes.Small);
            CurrentMode = ModeSmall;
        }
        public void ChangeModeToBig(PlayerController player)
        {
            ChangeMode(player, player.Profile.Modes.Big);
            CurrentMode = ModeBig;
        }
        public void ChangeModeToSuper(PlayerController player)
        {
            ChangeMode(player, player.Profile.Modes.Small);
            CurrentMode = ModeSuper;
        }
        #endregion

        #region Private
        private void ChangeMode(PlayerController Player, ScriptableObjects.Player.PlayerModeProfile playerMode)
        {
            Player.Animator.runtimeAnimatorController = playerMode.AnimatorController;
            Player.Movable.RaycastTop.Profile = playerMode.NormalRaycastRange.Top;
            Player.Movable.RaycastBottom.Profile = playerMode.NormalRaycastRange.Bottom;
            Player.Movable.RaycastLeft.Profile = playerMode.NormalRaycastRange.Left;
            Player.Movable.RaycastRight.Profile = playerMode.NormalRaycastRange.Right;
        }
        #endregion
    }
}