using Mario.Game.Player;

namespace Mario.Game.Interfaces
{
    public interface IHittableByPlayerFromTop
    {
        void OnHittedByPlayerFromTop(PlayerController player);
    }
}