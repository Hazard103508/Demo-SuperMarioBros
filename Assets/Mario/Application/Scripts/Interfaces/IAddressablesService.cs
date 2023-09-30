using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Application.Interfaces
{
    public interface IAddressablesService : IGameService
    {
        void AddAsset<T>(AssetReference AssetReference);
        T GetAssetReference<T>(AssetReference assetReference);
        void ReleaseAllAssets();
    }
}