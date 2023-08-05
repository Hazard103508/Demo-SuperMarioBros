using Mario.Application.Components;
using Mario.Application.Interfaces;
using Mario.Game.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mario.Application.Services
{
    public class PoolService : MonoBehaviour, IPoolService
    {
        private Dictionary<ObjectPoolTypes, ObjectPoolGroup> _poolGroups;

        public void LoadService()
        {
            _poolGroups = new Dictionary<ObjectPoolTypes, ObjectPoolGroup>();
        }

        public GameObject GetObjectFromPool(ObjectPoolTypes type)
        {
            var poolGroup = GetPoolGroup(type);
            return poolGroup.GetPoolObject();
        }

        public T GetObjectFromPool<T>(ObjectPoolTypes type) where T : MonoBehaviour
        {
            return GetObjectFromPool(type).GetComponent<T>();
        }

        private ObjectPoolGroup GetPoolGroup(ObjectPoolTypes type)
        {
            if (!_poolGroups.ContainsKey(type))
            {
                var obj = new GameObject(type.ToString() + "Pool");
                obj.transform.parent = transform;
                var group = obj.AddComponent<ObjectPoolGroup>();
                group.Type = type;
                _poolGroups.Add(type, group);
            }

            return _poolGroups[type];
        }
    }
}