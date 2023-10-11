using Mario.Application.Interfaces;
using Mario.Application.Services;

namespace Mario.Game.Player
{
    public abstract class PlayerStateNerf : PlayerState
    {
        #region Objects
        private readonly ITimeService _timeService;
        private readonly ISoundService _soundService;
        private readonly IPlayerService _playerService;
        #endregion

        #region Constructor
        public PlayerStateNerf(PlayerController player) : base(player)
        {
            _timeService = ServiceLocator.Current.Get<ITimeService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
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
            _soundService.Play(_playerService.PlayerProfile.Nerf.SoundFX);
            _timeService.FreezeTimer();
        }
        public override void Exit()
        {
            _timeService.ResumeTimer();
        }
        #endregion
    }
}