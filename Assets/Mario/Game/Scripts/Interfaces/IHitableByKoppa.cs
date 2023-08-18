using Mario.Game.Npc.Koopa;

namespace Mario.Game.Interfaces
{
    public interface IHitableByKoppa
    {
        void OnHittedByKoppa(Koopa koopa);
    }
}