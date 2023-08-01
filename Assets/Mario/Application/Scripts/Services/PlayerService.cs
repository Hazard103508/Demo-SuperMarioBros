using Mario.Application.Interfaces;
using Mario.Game.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class PlayerService : MonoBehaviour, IPlayerService
    {
        [SerializeField] private AudioSource _lifeUpSoundFX;
        [SerializeField] private AudioSource _deadSoundFX;
        private bool _canMove;

        public PlayerModes CurrentMode { get; set; }
        public bool CanMove
        {
            get => _canMove;
            set
            {
                _canMove = value;
                OnCanMoveChanged.Invoke();
            }
        }
        public int Lives { get; private set; }

        public UnityEvent OnCanMoveChanged { get; private set; }
        public UnityEvent OnLivesAdded { get; private set; }
        public UnityEvent OnLivesRemoved { get; private set; }

        public void LoadService()
        {
            OnLivesAdded = new UnityEvent();
            OnLivesRemoved = new UnityEvent();
            OnCanMoveChanged = new UnityEvent();

            Reset();
        }
        public void AddLife()
        {
            this.Lives++;
            _lifeUpSoundFX.Play();
            OnLivesAdded.Invoke();
        }
        public void Kill()
        {
            this.Lives--;
            _deadSoundFX.Play();
            OnLivesRemoved.Invoke();
            this.CurrentMode = PlayerModes.Small;

            AllServices.MusicService.Stop();
        }
        public void Reset()
        {
            CurrentMode = PlayerModes.Small;
            Lives = 3;
            CanMove = true;
        }
    }
}