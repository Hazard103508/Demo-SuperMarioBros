using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Mario.Application.Interfaces
{
    public interface IAddressablesService : IGameService
    {
        Task<AsyncOperationHandle<T>> LoadAssetAsync<T>(AssetReference assetReference);
        T GetAssetReference<T>(AssetReference assetReference);

        void ReleaseAllAssets();
    }
}