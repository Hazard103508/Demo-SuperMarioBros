using Mario.Application.Components;
using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Mario.Application.Services
{
    public class PoolService : MonoBehaviour, IPoolService
    {
        #region Objects
        private IAddressablesService _addressablesService;
        private ILevelService _levelService;

        private Dictionary<string, Pool> _poolGroups;
        private Dictionary<Type, PoolFactory> _poolFactories;
        #endregion

        #region Public Methods
        public void LoadService()
        {
            _addressablesService = ServiceLocator.Current.Get<IAddressablesService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();
            
            _poolGroups = new Dictionary<string, Pool>();
            _poolFactories = new Dictionary<Type, PoolFactory>();
        }
        public PooledObject GetObjectFromPool(PooledBaseProfile profile) => GetObjectFromPool(profile, Vector3.zero);
        public PooledObject GetObjectFromPool(PooledBaseProfile profile, Vector3 position) => GetPoolGroup(profile).Get(position);
        public T GetObjectFromPool<T>(PooledBaseProfile profile) where T : MonoBehaviour => GetObjectFromPool<T>(profile, Vector3.zero);
        public T GetObjectFromPool<T>(PooledBaseProfile profile, Vector3 position) where T : MonoBehaviour => GetObjectFromPool(profile, position).GetComponent<T>();
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
        private Pool GetPoolGroup(PooledBaseProfile profile)
        {
            string name = profile.name;
            if (!_poolGroups.ContainsKey(name))
            {
                var poolFactory = GetPoolFactory(profile);
                var newPool = poolFactory.CreatePool(profile, transform);
                _poolGroups.Add(name, newPool);
            }

            return _poolGroups[name];
        }
        private PoolFactory GetPoolFactory(PooledBaseProfile profile)
        {
            Type poolType = profile.GetType();
            if (!_poolFactories.ContainsKey(poolType))
            {
                var factory = (PoolFactory)(
                    poolType == typeof(PooledObjectProfile) ? new PoolFactoryObject() :
                    poolType == typeof(PooledSoundProfile) ? new PoolFactorySound() :
                    poolType == typeof(PooledUIProfile) ? new PoolFactoryUI() :
                    default);

                _poolFactories[poolType] = factory;
            }

            return _poolFactories[poolType];
        }
        //private void LoadPool(Pool pool, BasePooledObjectProfile profile)
        //{
        //    pool.CollectionCheck = profile.CollectionCheck;
        //    pool.DefaultCapacity = profile.DefaultCapacity;
        //    pool.MaxSize = profile.MaxSize;
        //    pool.Load();
        //}
        /*
        private void LoadWorldPool(Pool pool, BasePooledObjectProfile profile)
        {
            pool.PrefabReference = _addressablesService.GetAssetReference<GameObject>(profile.Reference);
            LoadItemPool(pool, profile);
        }
        private void LoadSoundPool(Pool pool, PooledSoundProfile profile)
        {
            var objRef = Instantiate(_addressablesService.GetAssetReference<GameObject>(profile.Reference));
            objRef.transform.parent = null;
            var audioSource = objRef.GetComponent<AudioSource>();
            audioSource.clip = profile.Clip;

            pool.PrefabReference = objRef;
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
        */
        #endregion
    }
}