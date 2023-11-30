using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface ISoundService : IGameService
    {
        void PlayTheme(PooledSoundProfile soundProfile);
        void PlayTheme(PooledSoundProfile soundProfile, float initTime);
        void StopTheme();

        void Play(PooledSoundProfile soundProfile);
        void Play(PooledSoundProfile soundProfile, Vector3 position);
        void Stop();
    }
}