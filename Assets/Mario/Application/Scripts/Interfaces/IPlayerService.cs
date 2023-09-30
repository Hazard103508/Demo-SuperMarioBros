using System;

namespace Mario.Application.Interfaces
{
    public interface IPlayerService : IGameService
    {
        int Lives { get; }
        bool CanMove { get; set; }

        event Action CanMoveChanged;
        event Action LivesAdded;
        event Action LivesRemoved;

        void AddLife();
        void RemoveLife();
        void Reset();
    }
}