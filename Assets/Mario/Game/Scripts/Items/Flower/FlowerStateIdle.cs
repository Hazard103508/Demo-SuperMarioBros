using Mario.Game.Player;

namespace Mario.Game.Items.Flower
{
    public class FlowerStateIdle : FlowerState
    {
        #region Constructor
        public FlowerStateIdle(Flower flower) : base(flower)
        {
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player) => CollectFlower(player);
        public override void OnHittedByPlayerFromBottom(PlayerController player) => CollectFlower(player);
        public override void OnHittedByPlayerFromLeft(PlayerController player) => CollectFlower(player);
        public override void OnHittedByPlayerFromRight(PlayerController player) => CollectFlower(player);
        #endregion
    }
}
