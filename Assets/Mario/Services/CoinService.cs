using Mario.Game.ScriptableObjects;
using Mario.Services.Interfaces;
using UnityEngine.Events;

namespace Mario.Services
{
    public class CoinService : ICoinService
    {
        private GameDataProfile _gameDataProfile;

        public CoinService(GameDataProfile gameDataProfile)
        {
            _gameDataProfile = gameDataProfile;
            OnCoinsChanged = new UnityEvent();
            Coins = 0;
        }
        
        public int Coins
        {
            get => _gameDataProfile.Coins;
            set
            {
                _gameDataProfile.Coins = value;
                OnCoinsChanged.Invoke();
            }
        }

        public UnityEvent OnCoinsChanged { get; set; }
    }
}