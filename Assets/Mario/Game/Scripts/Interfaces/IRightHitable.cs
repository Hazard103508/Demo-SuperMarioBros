using Mario.Game.Player;
namespace Mario.Game.Interfaces
{
    public interface IRightHitable
    {
        void OnHitFromRight(PlayerController player);
    }
}