using Mario.Commons.Structs;

namespace Mario.Game.Interfaces
{
    public interface IHittableByMovingToLeft
    {
        void OnHittedByMovingToLeft(RayHitInfo hitInfo);
    }
}

