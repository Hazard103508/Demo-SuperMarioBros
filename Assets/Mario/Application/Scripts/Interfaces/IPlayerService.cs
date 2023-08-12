using Mario.Game.Enums;
using System;
using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface IPlayerService : IGameService
    {
        PlayerModes CurrentMode { get; set; }
        int Lives { get; }
        bool CanMove { get; set; }

        event Action CanMoveChanged;
        event Action LivesAdded;
        event Action LivesRemoved;

        void AddLife();
        void Kill();
        void Reset();
    }
}