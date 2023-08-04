using Mario.Game.Player;

namespace Mario.Game.Interfaces
{
    public interface IHitableByPlayerFromBottom
    {
        void OnHitableByPlayerFromBottom(PlayerController player);
    }
}