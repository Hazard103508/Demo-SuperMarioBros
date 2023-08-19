using Mario.Game.Interfaces;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public interface IKoopaState : 
        IState,
        IHitableByPlayerFromTop,
        IHitableByPlayerFromBottom,
        IHitableByPlayerFromLeft,
        IHitableByPlayerFromRight,
        IHitableByBox,
        IHitableByKoppa,
        IHitableByFireBall
    {
        void OnLeftCollided(RayHitInfo hitInfo);
        void OnRightCollided(RayHitInfo hitInfo);
    }
}
