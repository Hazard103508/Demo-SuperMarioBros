namespace Mario.Application.Interfaces
{
    public interface ISceneService : IGameService
    {
        void LoadMapScene(float delay);
        void LoadMainScene();
        void LoadStandByScene();
        void LoadGameOverScene();
        void LoadTimeUpScene();
    }
}