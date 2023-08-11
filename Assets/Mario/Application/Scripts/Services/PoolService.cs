using Mario.Application.Components;
using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mario.Application.Services
{
    public class PoolService : MonoBehaviour, IPoolService
    {
        #region Objects
        private Dictionary<string, Pool> _poolGroups;
        #endregion

        #region Public Methods
        public void LoadService()
        {
            _poolGroups = new Dictionary<string, Pool>();
        }
        public PooledObject GetObjectFromPool(BasePooledObjectProfile profile)
        {
            var poolGroup = GetPoolGroup(profile.name);
            return poolGroup.Get();
        }
        public T GetObjectFromPool<T>(BasePooledObjectProfile profile) where T : MonoBehaviour
        {
            return GetObjectFromPool(profile).GetComponent<T>();
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
        #endregion

        #region Private Methods
        private Pool GetPoolGroup(string type)
        {
            if (!_poolGroups.ContainsKey(type))
            {
                var obj = new GameObject(type.ToString() + "Pool");
                obj.transform.parent = transform;

                var pool = obj.AddComponent<Pool>();
                _poolGroups.Add(type, pool);

                if (Services.GameDataService.CurrentMapProfile.ObjectsPool.PoolObjectsDic.ContainsKey(type))
                {
                    var poolItem = Services.GameDataService.CurrentMapProfile.ObjectsPool.PoolObjectsDic[type];
                    LoadObjectPool(pool, poolItem);
                }
                else
                {
                    var poolUI = Services.GameDataService.CurrentMapProfile.ObjectsPool.PooledUIDic[type];
                    LoadUIPool(pool, poolUI);
                }
            }

            return _poolGroups[type];
        }
        private void LoadBasePool(Pool pool, BasePooledObjectProfile profile)
        {
            var itemReference = Services.AddressablesService.GetAssetReference(profile.Reference);
            pool.PrefabReference = itemReference;
            pool.CollectionCheck = profile.CollectionCheck;
            pool.DefaultCapacity = profile.DefaultCapacity;
            pool.MaxSize = profile.MaxSize;
            pool.Load();
        }
        private void LoadObjectPool(Pool pool, PooledObjectProfile profile)
        {
            LoadBasePool(pool, profile);
        }
        private void LoadUIPool(Pool pool, PooledUIProfile profile)
        {
            LoadBasePool(pool, profile);

            var canvas = pool.gameObject.AddComponent<Canvas>();
            canvas.renderMode = profile.RenderMode;
            if (profile.RenderMode != RenderMode.ScreenSpaceOverlay)
                canvas.worldCamera = Camera.main;

            canvas.sortingLayerName = profile.CanvasSortingLayer;
        }
        #endregion
    }
}