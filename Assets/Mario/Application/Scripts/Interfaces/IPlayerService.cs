using System;

namespace Mario.Application.Interfaces
{
    public interface IPlayerService : IGameService
    {
        int Lives { get; }

        event Action LivesAdded;
        event Action LivesRemoved;

        void AddLife();
        void RemoveLife();
        void Reset();
    }
}