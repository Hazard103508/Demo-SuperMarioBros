using Mario.Game.Player;

namespace Mario.Game.Interfaces
{
    public interface ITopHitable
    {
        void OnHitFromTop(PlayerController player);
    }
}