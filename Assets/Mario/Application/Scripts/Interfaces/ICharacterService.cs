using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface ICharacterService : IGameService
    {
        bool CanMove { get; }
        UnityEvent<Vector3> OnPlayerPositionChanged { get; }


        void StopMovement();
        void ResumeMovement();
        void UpdatePlayerPositon(Vector3 position);
    }
}