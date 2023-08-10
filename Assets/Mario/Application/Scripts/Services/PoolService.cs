using Mario.Application.Components;
using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace Mario.Application.Services
{
    public class PoolService : MonoBehaviour, IPoolService
    {
        private Dictionary<string, ObjectPool> _poolGroups;

        public void LoadService()
        {
            _poolGroups = new Dictionary<string, ObjectPool>();
        }

        public PooledObject GetObjectFromPool(ObjectPoolProfile profile)
        {
            var poolGroup = GetPoolGroup(profile.name);
            return poolGroup.Get();
        }

        public T GetObjectFromPool<T>(ObjectPoolProfile profile) where T : MonoBehaviour
        {
            return GetObjectFromPool(profile).GetComponent<T>();
        }

        private ObjectPool GetPoolGroup(string type)
        {
            if (!_poolGroups.ContainsKey(type))
            {
                var obj = new GameObject(type.ToString() + "Pool");
                obj.transform.parent = transform;

                var poolItem = Services.GameDataService.CurrentMapProfile.ObjectsPool.PoolObjectsDic[type];
                var itemReference = Services.AddressablesService.GetAssetReference(poolItem.Reference);

                var pool = obj.AddComponent<ObjectPool>();
                pool.PrefabReference = itemReference;

                if (poolItem.RequireCanvas)
                {
                    var canvas = obj.AddComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceCamera;
                    canvas.worldCamera = Camera.main;
                    canvas.sortingLayerName = poolItem.CanvasSortingLayer;
                }

                _poolGroups.Add(type, pool);
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