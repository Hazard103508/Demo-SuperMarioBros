using Mario.Application.Interfaces;
using Mario.Game.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class CharacterService : ICharacterService
    {
        private Vector3 _playerPosition;

        public CharacterService()
        {
            CanMove = true;
            OnPlayerPositionChanged = new UnityEvent<Vector3>();
        }

        public PlayerModes CurrentPlayerMode { get; set; }
        public bool CanMove { get; private set; }

        public UnityEvent<Vector3> OnPlayerPositionChanged { get; private set; }

        public void StopMovement() => CanMove = false;
        public void ResumeMovement() => CanMove = true;
        public void UpdatePlayerPositon(Vector3 position)
        {
            if (_playerPosition != position)
            {
                _playerPosition = position;
                OnPlayerPositionChanged.Invoke(position);
            }
        }
    }
}