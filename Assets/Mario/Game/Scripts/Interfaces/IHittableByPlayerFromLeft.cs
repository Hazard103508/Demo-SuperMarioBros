using Mario.Game.Player;

namespace Mario.Game.Interfaces
{
    public interface IHittableByPlayerFromLeft
    {
        void OnHittedByPlayerFromLeft(PlayerController player);
    }
}