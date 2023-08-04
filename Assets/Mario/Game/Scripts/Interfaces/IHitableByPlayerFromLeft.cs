using Mario.Game.Player;
namespace Mario.Game.Interfaces
{
    public interface IHitableByPlayerFromLeft
    {
        void OnHitableByPlayerFromLeft(PlayerController player);
    }
}