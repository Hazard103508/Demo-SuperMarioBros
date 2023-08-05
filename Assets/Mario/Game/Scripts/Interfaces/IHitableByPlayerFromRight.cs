using Mario.Game.Player;
namespace Mario.Game.Interfaces
{
    public interface IHitableByPlayerFromRight
    {
        void OnHittedByPlayerFromRight(PlayerController player);
    }
}