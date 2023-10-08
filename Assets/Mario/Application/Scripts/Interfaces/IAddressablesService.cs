using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Mario.Application.Interfaces
{
    public interface IAddressablesService : IGameService
    {
        T GetAssetReference<T>(AssetReference assetReference);
        void LoadAsset<T>(AssetReference assetReference, Action<AsyncOperationHandle<T>> onCompleted);
        Task<AsyncOperationHandle<T>> LoadAssetAsync<T>(AssetReference assetReference);

        void ReleaseAllAssets();
    }
}