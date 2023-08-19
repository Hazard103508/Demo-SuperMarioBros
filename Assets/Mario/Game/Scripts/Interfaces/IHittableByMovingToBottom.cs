using UnityShared.Commons.Structs;

namespace Mario.Game.Interfaces
{
    public interface IHittableByMovingToBottom
    {
        void OnHittedByMovingToBottom(RayHitInfo hitInfo);
    }
}

