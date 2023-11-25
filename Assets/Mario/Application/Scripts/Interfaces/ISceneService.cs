namespace Mario.Application.Interfaces
{
    public interface ISceneService : IGameService
    {
        void LoadGameScene();
        void LoadMainScene();
        void LoadGameOverScene();
        void LoadTimeUpScene();
    }
}