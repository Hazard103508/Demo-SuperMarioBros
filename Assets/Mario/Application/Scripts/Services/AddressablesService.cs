using Mario.Application.Interfaces;
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
        private Dictionary<AssetReference, AsyncOperationHandle> _references;
        #endregion

        #region Public Methods
        public void LoadService()
        {
            _references = new Dictionary<AssetReference, AsyncOperationHandle>();
        }
        public T GetAssetReference<T>(AssetReference assetReference)
        {
            if (_references.ContainsKey(assetReference))
                return (T)_references[assetReference].Result;

            return default;
        }
        public Task<AsyncOperationHandle<T>> LoadAssetAsync<T>(AssetReference assetReference)
        {
            var taskCompletionSource = new TaskCompletionSource<AsyncOperationHandle<T>>();
            var asyncOperationHandle = assetReference.LoadAssetAsync<T>();
            asyncOperationHandle.Completed += handle => 
            { 
                taskCompletionSource.SetResult(handle); 
                _references.Add(assetReference, handle);
            };

            return Task.Run(() => taskCompletionSource.Task);
        }
        public void ReleaseAllAssets()
        {
            foreach (var item in _references)
                item.Key.ReleaseAsset();

            _references.Clear();
        }
        #endregion
    }
}