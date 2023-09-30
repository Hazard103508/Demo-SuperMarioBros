using Mario.Game.ScriptableObjects.Pool;

namespace Mario.Application.Interfaces
{
    public interface ISoundService : IGameService
    {
        void Play(PooledSoundProfile soundProfile);
    }
}