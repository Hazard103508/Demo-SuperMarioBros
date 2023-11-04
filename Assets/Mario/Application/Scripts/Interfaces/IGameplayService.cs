using Mario.Game.ScriptableObjects.Map;
using System;

namespace Mario.Application.Interfaces
{
    public interface IGameplayService : IGameService
    {
        event Action GameFreezed;
        event Action GameUnfreezed;

        void SetNextMap(MapConnection connection);
        void FreezeGame();
        void UnfreezeGame();
        void SetFlagReached();
        void SetHouseReached();
    }
}