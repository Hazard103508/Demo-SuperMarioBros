using Mario.Game.Enums;
using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface IPoolService : IGameService
    {
        GameObject GetObjectFromPool(ObjectPoolTypes type);
        T GetObjectFromPool<T>(ObjectPoolTypes type) where T : MonoBehaviour;
    }
}