using Mario.Application.Components;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface IPoolService : IGameService
    {
        PooledObject GetObjectFromPool(PooledBaseProfile profile);
        PooledObject GetObjectFromPool(PooledBaseProfile profile, Vector3 position);
        T GetObjectFromPool<T>(PooledBaseProfile profile) where T : MonoBehaviour;
        T GetObjectFromPool<T>(PooledBaseProfile profile, Vector3 position) where T : MonoBehaviour;
        void ClearPool();
    }
}