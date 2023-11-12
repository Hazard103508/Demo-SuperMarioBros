using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Pool;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Mario.Application
{
    public class AddressablesLoaderContainer
    {
        private IAddressablesService _addressablesService;
        private Dictionary<string, AssetReference> _references;
        private int _countTotal;
        private int _countLoaded;

        public event Action LoadCompleted;

        public AddressablesLoaderContainer()
        {
            _addressablesService = ServiceLocator.Current.Get<IAddressablesService>();
            _references = new Dictionary<string, AssetReference>();
        }

        public void Register<T>(T[] poolItems) where T : PooledBaseProfile
        {
            foreach (T item in poolItems)
            {
                Register<T>(item);
            }
        }
        public void Register<T>(T poolItem) where T : PooledBaseProfile => Register(poolItem.name, poolItem.Reference);
        public void Register(string key, AssetReference assetReference)
        {
            _references.Add(key, assetReference);
            _countTotal++;
        }

        public void LoadAssetAsync<T>(PooledBaseProfile[] poolItems)
        {
            foreach (PooledBaseProfile item in poolItems)
            {
                LoadAssetAsync<T>(item);
            }
        }
        public void LoadAssetAsync<T>(PooledBaseProfile poolItem) => LoadAssetAsync<T>(poolItem.name);
        public void LoadAssetAsync<T>(string key)
        {
            var assetReference = _references[key];
            _addressablesService.LoadAsset<T>(key, assetReference, OnLoadAssetAsyncCompleted);
        }
        public T GetAssetReference<T>(string key) => _addressablesService.GetAssetReference<T>(key);
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
            if (_countLoaded == _countTotal)
            {
                LoadCompleted?.Invoke();
            }
        }
    }
}