using UnityShared.Commons.Structs;

namespace Mario.Game.Interfaces
{
    public interface IHittableByMovingToTop
    {
        void OnHittedByMovingToTop(RayHitInfo hitInfo);
    }
}

