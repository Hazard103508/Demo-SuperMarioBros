using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Mario.Application.Interfaces
{
    public interface IAddressablesService : IGameService
    {
        T GetAssetReference<T>(string key, AssetReference assetReference);
        void LoadAsset<T>(string key, AssetReference assetReference, Action<AsyncOperationHandle<T>> onCompleted);
        Task<AsyncOperationHandle<T>> LoadAssetAsync<T>(string key, AssetReference assetReference);

        void ReleaseAllAssets();
    }
}