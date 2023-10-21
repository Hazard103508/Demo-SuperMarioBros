using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Boxes.Box;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Boxes.BrickBoxCoin
{
    public class BrickBoxCoinStateIdle : BoxStateIdle
    {
        #region Objects
        private readonly IPoolService _poolService;
        private readonly ISoundService _soundService;
        #endregion

        #region Properties
        new protected BrickBoxCoin Box => (BrickBoxCoin)base.Box;
        #endregion

        #region Constructor
        public BrickBoxCoinStateIdle(Box.Box box) : base(box)
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