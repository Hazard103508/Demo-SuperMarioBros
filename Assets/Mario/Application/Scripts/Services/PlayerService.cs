using Mario.Application.Interfaces;
using Mario.Game.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class PlayerService : MonoBehaviour, IPlayerService
    {
        private Vector3 _playerPosition;

        public PlayerModes CurrentPlayerMode { get; set; }
        public Vector3 PlayerPosition
        {
            get => _playerPosition;
            set
            {
                if (_playerPosition != value)
                {
                    _playerPosition = value;
                    OnPlayerPositionChanged.Invoke(value);
                }
            }
        }
        public int Lives { get; private set; }

        public UnityEvent OnLivesAdded { get; private set; }
        public UnityEvent OnLivesRemoved { get; private set; }
        public UnityEvent<Vector3> OnPlayerPositionChanged { get; private set; }

        public void LoadService()
        {
            Lives = 3;
            OnLivesAdded = new UnityEvent();
            OnLivesRemoved = new UnityEvent();
            OnPlayerPositionChanged = new UnityEvent<Vector3>();
        }
        public void AddLife()
        {
            this.Lives++;
            OnLivesAdded.Invoke();
        }
        public void RemoveLife()
        {
            this.Lives--;
            OnLivesRemoved.Invoke();
            this.CurrentPlayerMode = PlayerModes.Small;
        }
    }
}