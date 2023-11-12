using Mario.Application.Components;
using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using System;
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
        private Dictionary<Type, PoolFactory> _poolFactories;
        #endregion

        #region Public Methods
        public void Initalize()
        {
            _addressablesService = ServiceLocator.Current.Get<IAddressablesService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();

            _poolGroups = new Dictionary<string, Pool>();
            _poolFactories = new Dictionary<Type, PoolFactory>();
        }
        public void Dispose()
        {
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
                if (group != null && group.gameObject != null)
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
        #endregion
    }
}