using UnityShared.Commons.Structs;

namespace Mario.Game.Interfaces
{
    public interface IHittableByMovingToRight
    {
        void OnHittedByMovingToRight(RayHitInfo hitInfo);
    }
}

