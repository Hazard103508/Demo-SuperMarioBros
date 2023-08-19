using UnityShared.Commons.Structs;

namespace Mario.Game.Interfaces
{
    public interface IHittableByMovingToLeft
    {
        void OnHittedByMovingToLeft(RayHitInfo hitInfo);
    }
}

