using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Boxes.Box;
using Mario.Game.Player;

namespace Mario.Game.Boxes.MysteryBoxCoin
{
    public class BoxMysteryCoinStateIdle : BoxStateIdle
    {
        #region Objects
        private readonly IPoolService _poolService;
        private readonly ISoundService _soundService;
        #endregion

        #region Properties
        new protected BoxMysteryCoin Box => (BoxMysteryCoin)base.Box;
        #endregion

        #region Constructor
        public BoxMysteryCoinStateIdle(Box.Box box) : base(box)
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            Box.IsLastJump = true;
            _poolService.GetObjectFromPool(Box.Profile.CoinPoolReference, Box.transform.position);
            _soundService.Play(Box.Profile.HitSoundFXPoolReference, Box.transform.position);
            base.OnHittedByPlayerFromBottom(player);
        }
        #endregion
    }
}