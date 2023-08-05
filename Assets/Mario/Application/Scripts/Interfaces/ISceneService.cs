using UnityEngine.AddressableAssets;

namespace Mario.Application.Interfaces
{
    public interface ISceneService : IGameService
    {
        void LoadMapScene(float minDelay);
        void LoadMainScene();
        void LoadStandByScene();
        void LoadGameOverScene();
        void LoadTimeUpScene();
    }
}