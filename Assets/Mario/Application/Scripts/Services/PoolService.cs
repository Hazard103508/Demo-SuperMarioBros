using Mario.Application.Components;
using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        public PooledObject GetObjectFromPool(BasePooledObjectProfile profile) => GetObjectFromPool(profile, Vector3.zero);
        public PooledObject GetObjectFromPool(BasePooledObjectProfile profile, Vector3 position)
        {
            var poolGroup = GetPoolGroup(profile.name);
            return poolGroup.Get(position);
        }
        public T GetObjectFromPool<T>(BasePooledObjectProfile profile) where T : MonoBehaviour
        {
            return GetObjectFromPool<T>(profile, Vector3.zero);
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
        private void LoadItemPool(Pool pool, BasePooledObjectProfile profile)
        {
            pool.CollectionCheck = profile.CollectionCheck;
            pool.DefaultCapacity = profile.DefaultCapacity;
            pool.MaxSize = profile.MaxSize;
            pool.Load();
        }
        private void LoadObjectPool(Pool pool, PooledObjectProfile profile)
        {
            pool.PrefabReference = Services.AddressablesService.GetAssetReference<GameObject>(profile.Reference);
            LoadItemPool(pool, profile);
        }
        private void LoadSoundPool(Pool pool, PooledSoundProfile profile) 
        {
            GameObject obj = new GameObject(pool.name);
            var audioSource =  obj.AddComponent<AudioSource>();
            audioSource.clip = Services.AddressablesService.GetAssetReference<AudioClip>(profile.Reference);

            pool.PrefabReference = obj;
            LoadItemPool(pool, profile);
        }
        private void LoadUIPool(Pool pool, PooledUIProfile profile)
        {
            pool.PrefabReference = Services.AddressablesService.GetAssetReference<GameObject>(profile.Reference);
            LoadItemPool(pool, profile);

            var canvas = pool.gameObject.AddComponent<Canvas>();
            canvas.renderMode = profile.RenderMode;
            if (profile.RenderMode != RenderMode.ScreenSpaceOverlay)
                canvas.worldCamera = Camera.main;

            canvas.sortingLayerName = profile.CanvasSortingLayer;
        }
        #endregion
    }
}