using Mario.Game.ScriptableObjects.Map;

namespace Mario.Application.Interfaces
{
    public interface ILevelService : IGameService
    {
        MapProfile MapProfile { get; }
        bool IsLoadCompleted { get; }

        void LoadLevel();
        void LoadNextLevel();
        void UnloadLevel();
        void SetNextMap(MapConnection connection);
        void SetFlagReached();
        void SetHouseReached();
    }
}