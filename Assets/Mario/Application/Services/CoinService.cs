using Mario.Application.Interfaces;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class CoinService : ICoinService
    {
        private IGameDataService _gameDataService;

        public CoinService()
        {
            _gameDataService = ServiceLocator.Current.Get<IGameDataService>();
            OnCoinsChanged = new UnityEvent();
        }

        public UnityEvent OnCoinsChanged { get; set; }

        public void AddCoin()
        {
            _gameDataService.Coins++;
            OnCoinsChanged.Invoke();
        }
    }
}