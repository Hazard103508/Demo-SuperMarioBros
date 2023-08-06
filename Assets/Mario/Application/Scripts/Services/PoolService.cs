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
        private Dictionary<string, ObjectPoolGroup> _poolGroups;

        public void LoadService()
        {
            _poolGroups = new Dictionary<string, ObjectPoolGroup>();
        }

        public GameObject GetObjectFromPool(string type)
        {
            var poolGroup = GetPoolGroup(type);
            return poolGroup.GetPoolObject();
        }

        public T GetObjectFromPool<T>(string type) where T : MonoBehaviour
        {
            return GetObjectFromPool(type).GetComponent<T>();
        }

        private ObjectPoolGroup GetPoolGroup(string type)
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

        public void ClearPool()
        {
            foreach (var item in _poolGroups)
            {
                var group = item.Value;
                Destroy(group.gameObject);
            }

            _poolGroups.Clear();
        }
    }
}