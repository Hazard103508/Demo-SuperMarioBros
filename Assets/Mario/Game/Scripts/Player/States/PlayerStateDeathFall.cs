using Mario.Application.Interfaces;
using Mario.Application.Services;

namespace Mario.Game.Player
{
    public class PlayerStateDeathFall : PlayerState
    {
        #region Objects
        private readonly IPlayerService _playerService;
        private readonly ISoundService _soundService;
        private readonly IGameplayService _gameplayService;
        #endregion

        #region Constructor
        public PlayerStateDeathFall(PlayerController player) : base(player)
        {
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            _gameplayService.State = GameplayService.GameState.Lose;
            _soundService.StopTheme();
            _playerService.RemoveLife();
            _gameplayService.FreezeGame();
        }
        public override void Exit()
        {
            base.Exit();
            ChangeModeToSmall(Player);
        }
        #endregion
    }
}