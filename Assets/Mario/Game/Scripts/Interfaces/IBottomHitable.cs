using Mario.Game.Player;

namespace Mario.Game.Interfaces
{
    public interface IBottomHitable
    {
        void OnHitFromBottom(PlayerController player);
    }
}