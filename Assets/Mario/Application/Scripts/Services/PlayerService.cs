using Mario.Application.Interfaces;
using Mario.Game.Enums;
using System;
using UnityEngine;

namespace Mario.Application.Services
{
    public class PlayerService : MonoBehaviour, IPlayerService
    {
        #region Objects
        [SerializeField] private AudioSource _lifeUpSoundFX;
        [SerializeField] private AudioSource _deadSoundFX;
        private bool _canMove;
        #endregion

        #region Properties
        public PlayerModes CurrentMode { get; set; }
        public bool CanMove
        {
            get => _canMove;
            set
            {
                _canMove = value;
                CanMoveChanged?.Invoke();
            }
        }
        public int Lives { get; private set; }
        #endregion

        #region Events
        public event Action CanMoveChanged;
        public event Action LivesAdded;
        public event Action LivesRemoved;
        #endregion

        #region Public Methods
        public void LoadService()
        {
            Reset();
        }
        public void AddLife()
        {
            this.Lives++;
            _lifeUpSoundFX.Play();
            LivesAdded?.Invoke();
        }
        public void Kill()
        {
            this.Lives--;
            _deadSoundFX.Play();
            LivesRemoved?.Invoke();
            this.CurrentMode = PlayerModes.Small;

            Services.MusicService.Stop();
        }
        public void Reset()
        {
            CurrentMode = PlayerModes.Small;
            Lives = 3;
            CanMove = true;
        }
        #endregion
    }
}