namespace Mario.Application.Interfaces
{
    public interface ISceneService : IGameService
    {
        void LoadGameScene();
        void LoadMainScene();
        void LoadStandByScene();
        void LoadGameOverScene();
        void LoadTimeUpScene();
    }
}