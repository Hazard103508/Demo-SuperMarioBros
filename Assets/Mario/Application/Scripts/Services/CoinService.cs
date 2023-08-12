using Mario.Application.Interfaces;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class CoinService : MonoBehaviour, ICoinService
    {
        #region Objects
        [SerializeField] private AudioSource _coinSoundFX;
        #endregion

        #region Properties
        public event Action CoinsChanged;
        #endregion

        #region Events
        public int Coins { get; private set; }
        #endregion

        #region Public Methods
        public void LoadService()
        {
        }
        public void Add()
        {
            this.Coins++;
            _coinSoundFX.Play();

            if (this.Coins >= 100)
            {
                Services.PlayerService.AddLife();
                this.Coins = 0;
            }

            CoinsChanged.Invoke();
        }
        public void Reset()
        {
            Coins = 0;
        }
        #endregion
    }
}