using Mario.Application.Components;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface IPoolService : IGameService
    {
        PooledObject GetObjectFromPool(BasePooledObjectProfile profile);
        T GetObjectFromPool<T>(BasePooledObjectProfile profile) where T : MonoBehaviour;
        void ClearPool();
    }
}