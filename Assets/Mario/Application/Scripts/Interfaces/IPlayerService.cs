using Mario.Game.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface IPlayerService : IGameService
    {
        PlayerModes CurrentMode { get; set; }
        Vector3 Position { get; set; }
        int Lives { get; }
        bool CanMove { get; set; }

        UnityEvent OnLivesAdded { get; }
        UnityEvent OnLivesRemoved { get; }
        UnityEvent<Vector3> OnPositionChanged { get; }

        void AddLife();
        void RemoveLife();
    }
}