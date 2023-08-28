using Mario.Game.Interactable;

namespace Mario.Game.Interfaces
{
    public interface IHittableByFireBall
    {
        void OnHittedByFireBall(Fireball fireball);
    }
}