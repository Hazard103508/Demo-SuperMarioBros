using Mario.Application.Interfaces;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class ScoreService : IScoreService
    {
        private IGameDataService _gameDataService;

        public ScoreService()
        {
            _gameDataService = ServiceLocator.Current.Get<IGameDataService>();
            OnScoreChanged = new UnityEvent();
        }

        public UnityEvent OnScoreChanged { get; set; }

        public void Add(int points)
        {
            _gameDataService.Score += points;
            OnScoreChanged.Invoke();
        }
    }
}