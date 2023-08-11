using Mario.Application.Components;
using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace Mario.Application.Services
{
    public class PoolService : MonoBehaviour, IPoolService
    {
        private Dictionary<string, Pool> _poolGroups;

        public void LoadService()
        {
            _poolGroups = new Dictionary<string, Pool>();
        }

        public PooledObject GetObjectFromPool(PooledObjectProfile profile)
        {
            var poolGroup = GetPoolGroup(profile.name);
            return poolGroup.Get();
        }

        public T GetObjectFromPool<T>(PooledObjectProfile profile) where T : MonoBehaviour
        {
            return GetObjectFromPool(profile).GetComponent<T>();
        }

        private Pool GetPoolGroup(string type)
        {
            if (!_poolGroups.ContainsKey(type))
            {
                var obj = new GameObject(type.ToString() + "Pool");
                obj.transform.parent = transform;

                var poolItem = Services.GameDataService.CurrentMapProfile.ObjectsPool.PoolObjectsDic[type];
                var itemReference = Services.AddressablesService.GetAssetReference(poolItem.Reference);

                var pool = obj.AddComponent<Pool>();
                pool.PrefabReference = itemReference;
                pool.CollectionCheck = poolItem.CollectionCheck;
                pool.DefaultCapacity= poolItem.DefaultCapacity;
                pool.MaxSize = poolItem.MaxSize;
                pool.Load();

                if (poolItem.IsCanvasPool)
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