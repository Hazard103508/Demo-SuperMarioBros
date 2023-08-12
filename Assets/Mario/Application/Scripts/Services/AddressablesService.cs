using Mario.Application.Interfaces;
using System.Collections.Generic;
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
        public void AddAsset(AssetReference assetReference)
        {
            if (_references.ContainsKey(assetReference))
                return;

            _references.Add(assetReference, default);

            var asyncOperationHandle = assetReference.LoadAssetAsync<GameObject>();
            asyncOperationHandle.Completed += handle => _references[assetReference] = handle;
        }
        public GameObject GetAssetReference(AssetReference assetReference)
        {
            if (_references.ContainsKey(assetReference))
                return (GameObject)_references[assetReference].Result;

            return default;
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