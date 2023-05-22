using UnityEngine.AddressableAssets;

namespace Mario.Application.Interfaces
{
    public interface IAssetReferencesService : IGameService
    {
        void Add(AssetReference AssetReference);
        T GetObjectReference<T>(AssetReference assetReference);
        void ReleaseAllAsset();
    }
}