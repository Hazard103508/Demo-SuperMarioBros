using Mario.Application.Interfaces;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class CoinService : ICoinService
    {
        public CoinService()
        {
            OnCoinsChanged = new UnityEvent();
        }

        public UnityEvent OnCoinsChanged { get; set; }

        public void Update()
        {
        }
        public void Add()
        {
            AllServices.GameDataService.Coins++;
            OnCoinsChanged.Invoke();
        }
    }
}