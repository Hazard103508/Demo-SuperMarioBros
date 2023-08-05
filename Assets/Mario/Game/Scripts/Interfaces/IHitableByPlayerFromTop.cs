using Mario.Game.Player;

namespace Mario.Game.Interfaces
{
    public interface IHitableByPlayerFromTop
    {
        void OnHittedByPlayerFromTop(PlayerController player);
    }
}