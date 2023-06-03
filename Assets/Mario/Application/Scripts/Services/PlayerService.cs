using Mario.Application.Interfaces;
using Mario.Game.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class PlayerService : MonoBehaviour, IPlayerService
    {
        private Vector3 _playerPosition;

        public PlayerModes CurrentMode { get; set; }
        public Vector3 Position
        {
            get => _playerPosition;
            set
            {
                if (_playerPosition != value)
                {
                    _playerPosition = value;
                    OnPositionChanged.Invoke(value);
                }
            }
        }
        public bool CanMove { get; set; }
        public int Lives { get; private set; }

        public UnityEvent OnLivesAdded { get; private set; }
        public UnityEvent OnLivesRemoved { get; private set; }
        public UnityEvent<Vector3> OnPositionChanged { get; private set; }

        public void LoadService()
        {
            CanMove = true;
            Lives = 3;
            OnLivesAdded = new UnityEvent();
            OnLivesRemoved = new UnityEvent();
            OnPositionChanged = new UnityEvent<Vector3>();
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
            this.CurrentMode = PlayerModes.Small;

            AllServices.MusicService.Stop();
        }
    }
}