using Mario.Game.Npc;

namespace Mario.Game.Interfaces
{
    public interface IHitableByKoppa
    {
        void OnHittedByKoppa(Koopa koopa);
    }
}