using Mario.Application.Components;
using Mario.Game.Enums;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface IPoolService : IGameService
    {
        PooledObject GetObjectFromPool(ObjectPoolProfile profile);
        T GetObjectFromPool<T>(ObjectPoolProfile profile) where T : MonoBehaviour;
        void ClearPool();
    }
}