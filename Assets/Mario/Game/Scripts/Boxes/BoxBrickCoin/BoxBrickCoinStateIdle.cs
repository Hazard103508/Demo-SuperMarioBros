using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Boxes.Box;
using Mario.Game.Player;

namespace Mario.Game.Boxes.BrickBoxCoin
{
    public class BoxBrickCoinStateIdle : BoxStateIdle
    {
        #region Objects
        private readonly IPoolService _poolService;
        private readonly ISoundService _soundService;
        #endregion

        #region Properties
        new protected BoxBrickCoin Box => (BoxBrickCoin)base.Box;
        #endregion

        #region Constructor
        public BoxBrickCoinStateIdle(Box.Box box) : base(box)
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            if (!Box.IsTimerRunning)
                Box.IsTimerRunning = true;

            _poolService.GetObjectFromPool(Box.Profile.CoinPoolReference, Box.transform.position);
            _soundService.Play(Box.Profile.HitSoundFXPoolReference);
            base.OnHittedByPlayerFromBottom(player);
        }
        #endregion
    }
}