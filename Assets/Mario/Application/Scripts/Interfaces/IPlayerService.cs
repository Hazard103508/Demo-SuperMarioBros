using Mario.Game.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface IPlayerService : IGameService
    {
        PlayerModes CurrentPlayerMode { get; set; }
        Vector3 PlayerPosition { get; set; }
        int Lives { get; }

        UnityEvent OnLivesAdded { get; }
        UnityEvent OnLivesRemoved { get; }
        UnityEvent<Vector3> OnPlayerPositionChanged { get; }

        void AddLife();
        void RemoveLife();
    }
}