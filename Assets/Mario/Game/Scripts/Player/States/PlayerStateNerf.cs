using Mario.Application.Interfaces;
using Mario.Application.Services;

namespace Mario.Game.Player
{
    public abstract class PlayerStateNerf : PlayerState
    {
        #region Objects
        private readonly ISoundService _soundService;
        #endregion

        #region Constructor
        public PlayerStateNerf(PlayerController player) : base(player)
        {
            _soundService = ServiceLocator.Current.Get<ISoundService>();
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
            _soundService.Play(Player.Profile.Nerf.SoundFX);
        }
        #endregion
    }
}