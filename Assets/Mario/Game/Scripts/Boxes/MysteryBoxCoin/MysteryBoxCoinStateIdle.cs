using Mario.Application.Services;
using Mario.Game.Boxes.Box;
using Mario.Game.Player;

namespace Mario.Game.Boxes.MysteryBoxCoin
{
    public class MysteryBoxCoinStateIdle : BoxStateIdle
    {
        #region Properties
        new protected MysteryBoxCoin Box => (MysteryBoxCoin)base.Box;
        #endregion

        #region Constructor
        public MysteryBoxCoinStateIdle(Box.Box box) : base(box)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Box.IsLastJump = true;
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            Services.PoolService.GetObjectFromPool(Box.Profile.CoinPoolReference, Box.transform.position);
            base.OnHittedByPlayerFromBottom(player);
        }
        #endregion
    }
}