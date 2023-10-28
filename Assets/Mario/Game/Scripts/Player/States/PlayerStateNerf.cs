using Mario.Application.Interfaces;
using Mario.Application.Services;

namespace Mario.Game.Player
{
    public abstract class PlayerStateNerf : PlayerState
    {
        #region Objects
        private readonly ISoundService _soundService;
        private readonly IPlayerService _playerService;
        private IGameplayService _gameplayService;
        #endregion

        #region Constructor
        public PlayerStateNerf(PlayerController player) : base(player)
        {
            _soundService = ServiceLocator.Current.Get<ISoundService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
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
            _gameplayService.FreezeGame();
        }
        public override void Exit()
        {
            _gameplayService.UnfreezeGame();
        }
        #endregion
    }
}