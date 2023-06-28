using Mario.Application.Interfaces;
using Mario.Game.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class PlayerService : MonoBehaviour, IPlayerService
    {
        private Vector3 _playerPosition;
        [SerializeField] private AudioSource _lifeUpSoundFX;
        [SerializeField] private AudioSource _deadSoundFX;

        public PlayerModes CurrentMode { get; set; }
        public Vector3 Position
        {
            get => _playerPosition;
            set
            {
                if (_playerPosition != value)
                {
                    _playerPosition = value;
                }
            }
        }
        public bool CanMove { get; set; }
        public int Lives { get; private set; }

        public UnityEvent OnLivesAdded { get; private set; }
        public UnityEvent OnLivesRemoved { get; private set; }

        public void LoadService()
        {
            CanMove = true;
            Lives = 3;
            OnLivesAdded = new UnityEvent();
            OnLivesRemoved = new UnityEvent();
        }
        public void AddLife()
        {
            this.Lives++;
            _lifeUpSoundFX.Play();
            OnLivesAdded.Invoke();
        }
        public void RemoveLife()
        {
            this.Lives--;
            _deadSoundFX.Play();
            OnLivesRemoved.Invoke();
            this.CurrentMode = PlayerModes.Small;

            AllServices.MusicService.Stop();
        }
    }
}