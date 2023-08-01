using UnityEngine.AddressableAssets;

namespace Mario.Application.Interfaces
{
    public interface ISceneService : IGameService
    {
        void AddAsset(AssetReference AssetReference);
        T GetAssetReference<T>(AssetReference assetReference);
        void ReleaseAllAssets();
        void LoadMapScene(float minDelay);
        void LoadMainScene();
        void LoadStandByScene();
        void LoadGameOverScene();
        void LoadTimeUpScene();
    }
}