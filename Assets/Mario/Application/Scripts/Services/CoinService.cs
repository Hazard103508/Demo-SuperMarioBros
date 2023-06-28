using Mario.Application.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class CoinService : MonoBehaviour, ICoinService
    {
        [SerializeField] private AudioSource _coinSoundFX;

        public int Coins { get; private set; }

        public void LoadService()
        {
            OnCoinsChanged = new UnityEvent();
        }

        public UnityEvent OnCoinsChanged { get; set; }

        public void Add()
        {
            this.Coins++;
            _coinSoundFX.Play();

            if (this.Coins >= 100)
            {
                AllServices.PlayerService.AddLife();
                this.Coins = 0;
            }

            OnCoinsChanged.Invoke();
        }
    }
}