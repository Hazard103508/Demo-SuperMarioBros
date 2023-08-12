using Mario.Game.Player;

namespace Mario.Game.Interfaces
{
    public interface IHitableByFireBall
    {
        void OnHittedByFireBall(Fireball fireball);
    }
}