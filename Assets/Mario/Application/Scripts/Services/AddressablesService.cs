using Mario.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public T GetAssetReference<T>(AssetReference assetReference)
        {
            string key = assetReference.RuntimeKey.ToString();
            if (_operationsHandle.ContainsKey(assetReference.RuntimeKey.ToString()))
                return (T)_operationsHandle[key].Result;

            return default;
        }
        public void LoadAsset<T>(AssetReference assetReference, Action<AsyncOperationHandle<T>> onCompleted)
        {
            string key = assetReference.RuntimeKey.ToString();
            if (_operationsHandle.ContainsKey(key))
                return;

            var asyncOperationHandle = assetReference.LoadAssetAsync<T>();
            asyncOperationHandle.Completed += handle => onCompleted?.Invoke(handle);

            _operationsHandle.Add(key, asyncOperationHandle);
            _references.Add(key, assetReference);
        }
        public Task<AsyncOperationHandle<T>> LoadAssetAsync<T>(AssetReference assetReference)
        {
            string key = assetReference.RuntimeKey.ToString();
            if (_operationsHandle.ContainsKey(key))
                return default;

            var taskCompletionSource = new TaskCompletionSource<AsyncOperationHandle<T>>();
            var asyncOperationHandle = assetReference.LoadAssetAsync<T>();
            asyncOperationHandle.Completed += handle => taskCompletionSource.SetResult(handle);
            _operationsHandle.Add(key, asyncOperationHandle);
            _references.Add(key, assetReference);

            return Task.Run(() => taskCompletionSource.Task);
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