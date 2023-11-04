using Mario.Game.ScriptableObjects.Map;
using System;

namespace Mario.Application.Interfaces
{
    public interface ILevelService : IGameService
    {
        MapProfile MapProfile { get; }
        bool IsLoadCompleted { get; }

        event Action LoadCompleted;
        event Func<MapProfile> LoadingConnection;

        void LoadLevel();
        void LoadNextLevel();
        void UnloadLevel();
    }
}