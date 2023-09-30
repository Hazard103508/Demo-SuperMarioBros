using Mario.Application.Interfaces;
using Mario.Game.Enums;
using Mario.Game.ScriptableObjects.Pool;
using System;
using UnityEngine;

namespace Mario.Application.Services
{
    public class PlayerService : MonoBehaviour, IPlayerService
    {
        #region Objects
        private ISoundService _soundService;

        private bool _canMove;
        [SerializeField] private PooledSoundProfile _1UpSoundPoolReference;
        [SerializeField] private PooledSoundProfile _deadSoundPoolReference;
        #endregion

        #region Properties
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
            _soundService= ServiceLocator.Current.Get<ISoundService>();
            Reset();
        }
        public void AddLife()
        {
            this.Lives++;
            _soundService.Play(_1UpSoundPoolReference);
            LivesAdded?.Invoke();
        }
        public void RemoveLife()
        {
            this.Lives--;
            _soundService.Play(_deadSoundPoolReference);
            LivesRemoved?.Invoke();

            Services.MusicService.Stop();
        }
        public void Reset()
        {
            Lives = 3;
            CanMove = true;
        }
        #endregion
    }
}