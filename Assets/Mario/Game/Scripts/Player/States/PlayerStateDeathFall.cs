using Mario.Application.Interfaces;
using Mario.Application.Services;

namespace Mario.Game.Player
{
    public class PlayerStateDeathFall : PlayerState
    {
        #region Objects
        private readonly IPlayerService _playerService;
        private readonly ISoundService _soundService;
        #endregion

        #region Constructor
        public PlayerStateDeathFall(PlayerController player) : base(player)
        {
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            _soundService.StopTheme();
            _playerService.RemoveLife();
            // frizzar tiempo
        }
        #endregion
    }
}