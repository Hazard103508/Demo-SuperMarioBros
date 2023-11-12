using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Mario.Application.Interfaces
{
    public interface IAddressablesService : IGameService
    {
        T GetAssetReference<T>(string key);
        void LoadAsset<T>(string key, AssetReference assetReference, Action<AsyncOperationHandle<T>> onCompleted);

        void ReleaseAllAssets();
    }
}