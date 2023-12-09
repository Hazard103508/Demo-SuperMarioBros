using Mario.Game.ScriptableObjects.Map;
using System;
using static Mario.Application.Services.GameplayService;

namespace Mario.Application.Interfaces
{
    public interface IGameplayService : IGameService
    {
        event Action GameFrozen;
        event Action GameUnfrozen;

        bool IsFrozen { get; }
        bool IsStarman { get; }
        GameState State { get; set; }

        void SetNextMap(MapConnection connection);
        void SetCheckPoint(MapProfile mapProfile);
        void FreezeGame();
        void UnfreezeGame();
        void SetFlagReached();
        void ActivateStarman(Action callback);
        void SetHouseReached();
    }
}