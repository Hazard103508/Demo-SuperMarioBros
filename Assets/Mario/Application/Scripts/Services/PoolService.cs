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
        private IAddressablesService _addressablesService;
        private ILevelService _levelService;

        private Dictionary<string, Pool> _poolGroups;
        #endregion

        #region Public Methods
        public void LoadService()
        {
            _addressablesService = ServiceLocator.Current.Get<IAddressablesService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();
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

                if (_levelService.CurrentMapProfile.PoolProfile.WorldPoolProfiles.ContainsKey(type))
                {
                    var poolItem = _levelService.CurrentMapProfile.PoolProfile.WorldPoolProfiles[type];
                    LoadWorldPool(pool, poolItem);
                }
                else
                {
                    var poolUI = _levelService.CurrentMapProfile.PoolProfile.UIPoolProfiles[type];
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
        private void LoadWorldPool(Pool pool, BasePooledObjectProfile profile)
        {
            pool.PrefabReference = _addressablesService.GetAssetReference<GameObject>(profile.Reference);
            LoadItemPool(pool, profile);
        }
        private void LoadUIPool(Pool pool, PooledUIProfile profile)
        {
            pool.PrefabReference = _addressablesService.GetAssetReference<GameObject>(profile.Reference);
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