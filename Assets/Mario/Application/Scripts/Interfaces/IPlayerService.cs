using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Player;
using System;
using System.Numerics;

namespace Mario.Application.Interfaces
{
    public interface IPlayerService : IGameService
    {
        PlayerProfile PlayerProfile { get; }
        PlayerController PlayerController { get; set; }
        int Lives { get; }

        event Action LivesAdded;
        event Action LivesRemoved;

        void AddLife();
        void RemoveLife();
        void Reset();
    }
}