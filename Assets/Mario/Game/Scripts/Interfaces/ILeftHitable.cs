using Mario.Game.Player;
namespace Mario.Game.Interfaces
{
    public interface ILeftHitable
    {
        void OnHitLeft(PlayerController player);
    }
}