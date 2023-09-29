using Mario.Application.Components;
using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using System.Collections.Generic;
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
        public PooledObject GetObjectFromPool(BasePooledObjectProfile profile, Vector3 position)
        {
            var poolGroup = GetPoolGroup(profile.name);
            return poolGroup.Get(position);
        }
        public T GetObjectFromPool<T>(BasePooledObjectProfile profile, Vector3 position) where T : MonoBehaviour
        {
            return GetObjectFromPool(profile, position).GetComponent<T>();
        }
        public void ClearPool()
        {
            foreach (var item in _poolGroups)
            {
                var group = item.Value;
                if (group.gameObject != null)
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

                if (Services.GameDataService.CurrentMapProfile.PoolProfile.PoolObjectsDic.ContainsKey(type))
                {
                    var poolItem = Services.GameDataService.CurrentMapProfile.PoolProfile.PoolObjectsDic[type];
                    LoadObjectPool(pool, poolItem);
                }
                else if (Services.GameDataService.CurrentMapProfile.PoolProfile.PoolSoundDic.ContainsKey(type))
                {
                    var poolItem = Services.GameDataService.CurrentMapProfile.PoolProfile.PoolSoundDic[type];
                    LoadSoundPool(pool, poolItem);
                }
                else
                {
                    var poolUI = Services.GameDataService.CurrentMapProfile.PoolProfile.PooledUIDic[type];
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
        private void LoadObjectPool(Pool pool, PooledObjectProfile profile) => LoadBasePool(pool, profile);
        private void LoadSoundPool(Pool pool, PooledSoundProfile profile) => LoadBasePool(pool, profile);
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