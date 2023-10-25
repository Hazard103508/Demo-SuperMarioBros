using System;

namespace Mario.Application.Interfaces
{
    public interface IPauseService : IGameService
    {
        bool IsPaused { get; }

        void Pause();
        void Resume();
    }
}