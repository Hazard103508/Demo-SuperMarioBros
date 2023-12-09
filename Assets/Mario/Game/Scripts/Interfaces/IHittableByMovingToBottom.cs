using Mario.Commons.Structs;

namespace Mario.Game.Interfaces
{
    public interface IHittableByMovingToBottom
    {
        void OnHittedByMovingToBottom(RayHitInfo hitInfo);
    }
}

