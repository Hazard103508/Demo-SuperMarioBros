using Mario.Application.Interfaces;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class CoinService : ICoinService
    {
        public int Coins { get; private set; }

        public CoinService()
        {
            OnCoinsChanged = new UnityEvent();
        }

        public UnityEvent OnCoinsChanged { get; set; }

        public void Add()
        {
            this.Coins++;
            OnCoinsChanged.Invoke();
        }
    }
}