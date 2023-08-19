using Mario.Game.Npc.Koopa;

namespace Mario.Game.Interfaces
{
    public interface IHittableByKoppa
    {
        void OnHittedByKoppa(Koopa koopa);
    }
}