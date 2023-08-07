using Mario.Application.Components;
using Mario.Application.Interfaces;
using Mario.Game.Enums;
using Mario.Game.ScriptableObjects.Pool;
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

        public GameObject GetObjectFromPool(ObjectPoolProfile profile)
        {
            var poolGroup = GetPoolGroup(profile.name);
            return poolGroup.GetPoolObject();
        }

        public T GetObjectFromPool<T>(ObjectPoolProfile profile) where T : MonoBehaviour
        {
            return GetObjectFromPool(profile).GetComponent<T>();
        }

        private ObjectPoolGroup GetPoolGroup(string type)
        {
            if (!_poolGroups.ContainsKey(type))
            {
                var obj = new GameObject(type.ToString() + "Pool");
                obj.transform.parent = transform;
                
                var group = obj.AddComponent<ObjectPoolGroup>();
                group.Type = type;

                var poolItem = Services.GameDataService.CurrentMapProfile.ObjectsPool.PoolObjectsDic[type];
                if (poolItem.RequireCanvas)
                {
                    var canvas = obj.AddComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceCamera;
                    canvas.worldCamera = Camera.main;
                    canvas.sortingLayerName = poolItem.CanvasSortingLayer;
                }

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