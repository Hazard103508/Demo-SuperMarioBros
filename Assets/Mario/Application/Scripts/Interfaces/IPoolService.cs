using Mario.Game.Enums;
using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface IPoolService : IGameService
    {
        GameObject GetObjectFromPool(string type);
        T GetObjectFromPool<T>(string type) where T : MonoBehaviour;
        void ClearPool();
    }
}