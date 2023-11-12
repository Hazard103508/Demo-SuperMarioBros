using Mario.Game.ScriptableObjects.Map;
using System;

namespace Mario.Application.Interfaces
{
    public interface ILevelService : IGameService
    {
        MapProfile MapProfile { get; }
        bool IsLoadCompleted { get; }

        event Action StartLoading;
        event Action LoadCompleted;

        void LoadLevel();
        void LoadNextLevel();
        void UnloadLevel();
        void SetMap(string mapName);
    }
}