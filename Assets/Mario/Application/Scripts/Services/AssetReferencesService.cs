using Mario.Application.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Mario.Application.Services
{
    public class AssetReferencesService : IAssetReferencesService
    {
        private Dictionary<AssetReference, AsyncOperationHandle> _references;

        public AssetReferencesService()
        {
            _references = new Dictionary<AssetReference, AsyncOperationHandle>();
        }

        public void Add(AssetReference assetReference)
        {
            if (_references.ContainsKey(assetReference))
                return;

            _references.Add(assetReference, default);

            var asyncOperationHandle = assetReference.LoadAssetAsync<GameObject>();
            asyncOperationHandle.Completed += handle => _references[assetReference] = handle;
        }
        public T GetObjectReference<T>(AssetReference assetReference)
        {
            if (_references.ContainsKey(assetReference))
                return (T)_references[assetReference].Result;

            return default;
        }
        public void ReleaseAllAsset()
        {
            foreach (var item in _references)
                item.Key.ReleaseAsset();

            _references.Clear();
        }
    }
}