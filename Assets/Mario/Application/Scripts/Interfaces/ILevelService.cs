using Mario.Game.ScriptableObjects.Map;
using System;
using static Mario.Application.Services.LevelService;

namespace Mario.Application.Interfaces
{
    public interface ILevelService : IGameService
    {
        MapProfile MapProfile { get; }
        bool IsLoadCompleted { get; }

        event Action<StartLoadingEvent> StartLoading;
        event Action LoadCompleted;

        void LoadLevel(bool showStandby);
        void UnloadLevel();
        void SetMap(MapProfile mapProfile);
        void Reset();
    }
}