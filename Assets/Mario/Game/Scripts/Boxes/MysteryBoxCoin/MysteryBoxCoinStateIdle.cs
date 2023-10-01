using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Boxes.Box;
using Mario.Game.Player;

namespace Mario.Game.Boxes.MysteryBoxCoin
{
    public class MysteryBoxCoinStateIdle : BoxStateIdle
    {
        #region Objects
        private readonly IPoolService _poolService;
        #endregion

        #region Properties
        new protected MysteryBoxCoin Box => (MysteryBoxCoin)base.Box;
        #endregion

        #region Constructor
        public MysteryBoxCoinStateIdle(Box.Box box) : base(box)
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            IsLastJump = true;
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            _poolService.GetObjectFromPool(Box.Profile.CoinPoolReference, Box.transform.position);
            base.OnHittedByPlayerFromBottom(player);
        }
        #endregion
    }
}