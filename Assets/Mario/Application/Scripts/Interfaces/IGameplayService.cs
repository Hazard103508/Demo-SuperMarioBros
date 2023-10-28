using System;

namespace Mario.Application.Interfaces
{
    public interface IGameplayService : IGameService
    {
        event Action GameFreezed;
        event Action GameUnfreezed;

        void FreezeGame();
        void UnfreezeGame();
    }
}