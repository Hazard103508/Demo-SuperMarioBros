using Mario.Commons.Structs;

namespace Mario.Game.Interfaces
{
    public interface IHittableByMovingToRight
    {
        void OnHittedByMovingToRight(RayHitInfo hitInfo);
    }
}

