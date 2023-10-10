using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Pool;

namespace Mario.Application
{
    public class AddressablesLoaderContainer
    {
        private IAddressablesService _addressablesService;
        private Dictionary<Type, List<PooledBaseProfile>> _references;

        private int _countTotal;
        private int _countLoaded;

        public event Action LoadCompleted;

        public bool IsLoadCompleted { get; private set; }

        public AddressablesLoaderContainer()
        {
            _addressablesService = ServiceLocator.Current.Get<IAddressablesService>();
            _references = new Dictionary<Type, List<PooledBaseProfile>>();
        }

        public void Register<T, R>(T[] poolItems) where T : PooledBaseProfile
        {
            foreach (T item in poolItems)
            {
                Register<T, R>(item);
            }
        }
        public void Register<T, R>(T poolItem) where T : PooledBaseProfile
        {
            Type type = typeof(R);
            if (!_references.ContainsKey(type))
                _references[type] = new List<PooledBaseProfile>();

            _countTotal++;
            _references[type].Add(poolItem);
        }
        public void LoadAssetAsync<T>()
        {
            var poolItems = _references[typeof(T)];

            foreach (var item in poolItems)
            {
                _addressablesService.LoadAsset<T>(item.Reference, OnLoadAssetAsyncCompleted);
            }
        }
        public void Clear()
        {
            _countTotal = 0;
            _countLoaded = 0;
            _references.Clear();
            _addressablesService.ReleaseAllAssets();
        }

        private void OnLoadAssetAsyncCompleted<T>(AsyncOperationHandle<T> asyncOperationHandle)
        {
            _countLoaded++;
            UnityEngine.Debug.Log($"{_countLoaded} - {asyncOperationHandle.Result}");

            if (_countLoaded == _countTotal)
            {
                IsLoadCompleted = true;
                LoadCompleted?.Invoke();
            }
        }
    }
}