using Mario.Game.Player;

namespace Mario.Game.Interfaces
{
    public interface IHittableByFireBall
    {
        void OnHittedByFireBall(Fireball fireball);
    }
}