using Mario.Application.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace Mario.Application.Services
{
    public class AddressablesService : MonoBehaviour, IAddressablesService
    {
        #region Objects
        private Dictionary<string, AsyncOperationHandle> _operationsHandle;
        private Dictionary<string, AssetReference> _references;
        #endregion

        #region Public Methods
        public void Initalize()
        {
            _operationsHandle = new Dictionary<string, AsyncOperationHandle>();
            _references = new Dictionary<string, AssetReference>();
        }
        public void Dispose()
        {
        }

        public T GetAssetReference<T>(string key) => (T)_operationsHandle[key].Result;
        public void LoadAsset<T>(string key, AssetReference assetReference, Action<AsyncOperationHandle<T>> onCompleted)
        {
            var asyncOperationHandle = assetReference.LoadAssetAsync<T>();
            asyncOperationHandle.Completed += handle => onCompleted?.Invoke(handle);

            _operationsHandle.Add(key, asyncOperationHandle);
            _references.Add(key, assetReference);
        }
        public void ReleaseAllAssets()
        {
            foreach (var item in _references)
                item.Value.ReleaseAsset();

            _operationsHandle.Clear();
            _references.Clear();
        }
        #endregion
    }
}