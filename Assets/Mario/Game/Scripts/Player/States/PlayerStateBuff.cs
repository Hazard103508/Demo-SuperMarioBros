using Mario.Application.Interfaces;
using Mario.Application.Services;

namespace Mario.Game.Player
{
    public abstract class PlayerStateBuff : PlayerState
    {
        #region Objects
        private readonly ISoundService _soundService;
        private readonly IPlayerService _playerService;
        private readonly IGameplayService _gameplayService;
        #endregion

        #region Constructor
        public PlayerStateBuff(PlayerController player) : base(player)
        {
            _soundService = ServiceLocator.Current.Get<ISoundService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Buff";
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            _gameplayService.FreezeGame();
            _soundService.Play(_playerService.PlayerProfile.Buff.SoundFX);
        }
        public override void Exit()
        {
            _gameplayService.UnfreezeGame();
        }
        #endregion
    }
}