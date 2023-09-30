using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface ISoundService : IGameService
    {
        void Play(PooledSoundProfile soundProfile);
        void Play(PooledSoundProfile soundProfile, Vector3 position);
    }
}