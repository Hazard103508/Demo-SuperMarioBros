using Mario.Game.ScriptableObjects.Map;
using System;
using static Mario.Application.Services.GameplayService;

namespace Mario.Application.Interfaces
{
    public interface IGameplayService : IGameService
    {
        event Action GameFreezed;
        event Action GameUnfreezed;

        GameState State { get; set; }

        void SetNextMap(MapConnection connection);
        void SetCheckPoint(MapProfile mapProfile);
        void FreezeGame();
        void UnfreezeGame();
        void SetFlagReached();
        void SetHouseReached();
    }
}