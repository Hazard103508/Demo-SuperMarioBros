using Mario.Game.Player;

namespace Mario.Game.Interfaces
{
    public interface IHittableByPlayerFromBottom
    {
        void OnHittedByPlayerFromBottom(PlayerController player);
    }
}