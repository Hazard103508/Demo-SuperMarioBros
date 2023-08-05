using Mario.Application.Interfaces;
using Mario.Game.Enums;
using Mario.Game.ScriptableObjects.Map;
using System;
using UnityEngine;

namespace Mario.Application.Services
{
    public class PoolService : MonoBehaviour, IPoolService
    {
        [SerializeField] private ObjectPoolProfile _profile;

        public void LoadService()
        {
            
        }

        public GameObject GetObjectFromPool(ObjectPoolTypes type)
        {
            throw new System.NotImplementedException();
        }

        public T GetObjectFromPool<T>(ObjectPoolTypes type) where T : MonoBehaviour
        {
            throw new NotImplementedException();
        }
    }
}