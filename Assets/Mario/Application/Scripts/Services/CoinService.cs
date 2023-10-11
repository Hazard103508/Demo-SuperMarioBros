using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using System;
using UnityEngine;

namespace Mario.Application.Services
{
    public class CoinService : MonoBehaviour, ICoinService
    {
        #region Objects
        private ISoundService _soundService;
        private IPlayerService _playerService;

        [SerializeField] private PooledSoundProfile _soundPoolReference;
        #endregion

        #region Properties
        public int Coins { get; private set; }
        #endregion

        #region Events
        public event Action CoinsChanged;
        #endregion

        #region Public Methods
        public void Initalize()
        {
            _soundService = ServiceLocator.Current.Get<ISoundService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
        }
        public void Dispose()
        {
        }
        public void Add()
        {
            this.Coins++;
            _soundService.Play(_soundPoolReference);

            if (this.Coins >= 100)
            {
                _playerService.AddLife();
                this.Coins = 0;
            }

            CoinsChanged?.Invoke();
        }
        public void Reset()
        {
            Coins = 0;
        }
        #endregion
    }
}