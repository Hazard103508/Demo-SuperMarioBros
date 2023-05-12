using Mario.Game.Player;

namespace Mario.Game.Interfaces
{
    public interface IBottomHitable
    {
        void OnHitBottom(PlayerController player);
    }
}