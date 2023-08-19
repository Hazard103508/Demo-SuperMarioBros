using Mario.Game.Interfaces;

namespace Mario.Game.Npc.Koopa
{
    public interface IKoopaState :
        IState,
        IHittableByMovingToLeft,
        IHittableByMovingToRight,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByBox,
        IHittableByKoppa,
        IHittableByFireBall
    {

    }
}
