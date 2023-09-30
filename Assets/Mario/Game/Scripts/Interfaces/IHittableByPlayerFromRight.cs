using Mario.Game.Player;

namespace Mario.Game.Interfaces
{
    public interface IHittableByPlayerFromRight
    {
        void OnHittedByPlayerFromRight(PlayerController player);
    }
}