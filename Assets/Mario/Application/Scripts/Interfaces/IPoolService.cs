using Mario.Application.Components;
using Mario.Game.Enums;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface IPoolService : IGameService
    {
        PooledObject GetObjectFromPool(PooledObjectProfile profile);
        T GetObjectFromPool<T>(PooledObjectProfile profile) where T : MonoBehaviour;
        void ClearPool();
    }
}