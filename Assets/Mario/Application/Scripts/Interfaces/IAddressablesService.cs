using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Mario.Application.Interfaces
{
    public interface IAddressablesService : IGameService
    {
        Task AddAsset<T>(AssetReference AssetReference);
        T GetAssetReference<T>(AssetReference assetReference);
        void ReleaseAllAssets();
    }
}