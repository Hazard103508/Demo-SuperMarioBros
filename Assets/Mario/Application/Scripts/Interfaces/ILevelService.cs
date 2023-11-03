using Mario.Game.ScriptableObjects.Map;
using System;

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
    }
}