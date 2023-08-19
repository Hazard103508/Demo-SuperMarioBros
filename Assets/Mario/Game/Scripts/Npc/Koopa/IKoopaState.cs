using Mario.Game.Interfaces;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public interface IKoopaState : 
        IState,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByBox,
        IHittableByKoppa,
        IHittableByFireBall
    {
        void OnLeftCollided(RayHitInfo hitInfo);
        void OnRightCollided(RayHitInfo hitInfo);
    }
}
