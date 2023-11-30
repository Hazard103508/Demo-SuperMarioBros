using System;

namespace Mario.Application.Interfaces
{
    public interface IPauseService : IGameService
    {
        bool IsPaused { get; }

        event Action Paused;
        event Action Resumed;

        void Pause();
        void Resume();
    }
}