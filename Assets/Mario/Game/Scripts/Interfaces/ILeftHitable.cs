using Mario.Game.Player;
namespace Mario.Game.Interfaces
{
    public interface ILeftHitable
    {
        void OnHitFromLeft(PlayerController player);
    }
}