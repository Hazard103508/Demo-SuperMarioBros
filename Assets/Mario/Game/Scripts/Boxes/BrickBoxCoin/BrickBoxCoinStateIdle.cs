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
        private readonly IAddressablesService _addressablesService;

        private float _limitTime;
        private bool _started;
        #endregion

        #region Properties
        new protected BrickBoxCoin Box => (BrickBoxCoin)base.Box;
        #endregion

        #region Constructor
        public BrickBoxCoinStateIdle(Box.Box box) : base(box)
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
            _addressablesService = ServiceLocator.Current.Get<IAddressablesService>();

            _addressablesService.LoadAssetAsync<GameObject>(Box.Profile.CoinPoolReference.Reference);
        }
        #endregion

        #region IState Methods
        public override void Update()
        {
            base.Update();

            if (_started)
            {
                _limitTime -= Time.deltaTime;
                if (_limitTime < 0)
                    IsLastJump = true;
            }
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            if (!_started)
            {
                _started = true;
                _limitTime = Box.Profile.LimitTime;
            }
            _poolService.GetObjectFromPool(Box.Profile.CoinPoolReference, Box.transform.position);
            base.OnHittedByPlayerFromBottom(player);
        }
        #endregion
    }
}