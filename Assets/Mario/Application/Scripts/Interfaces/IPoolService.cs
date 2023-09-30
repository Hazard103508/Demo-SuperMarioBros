using Mario.Application.Components;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface IPoolService : IGameService
    {
        PooledObject GetObjectFromPool(BasePooledObjectProfile profile);
        PooledObject GetObjectFromPool(BasePooledObjectProfile profile, Vector3 position);
        T GetObjectFromPool<T>(BasePooledObjectProfile profile) where T : MonoBehaviour;
        T GetObjectFromPool<T>(BasePooledObjectProfile profile, Vector3 position) where T : MonoBehaviour;
        void ClearPool();
    }
}