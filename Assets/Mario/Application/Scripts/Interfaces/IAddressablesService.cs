using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Application.Interfaces
{
    public interface IAddressablesService : IGameService
    {
        void AddAsset(AssetReference AssetReference);
        GameObject GetAssetReference(AssetReference assetReference);
        void ReleaseAllAssets();
    }
}