using Mario.Application.Interfaces;
using Mario.Game.Props;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class ScoreService : IScoreService
    {
        private IGameDataService _gameDataService;
        private TargetPoints _targetPointsPrefab;
        public ScoreService(TargetPoints targetPointsPrefab)
        {
            _gameDataService = ServiceLocator.Current.Get<IGameDataService>();
            _targetPointsPrefab = targetPointsPrefab;
            OnScoreChanged = new UnityEvent();
        }

        public UnityEvent OnScoreChanged { get; set; }

        public void Update()
        {
        }
        public void Add(int points)
        {
            _gameDataService.Score += points;
            OnScoreChanged.Invoke();
        }
        public void ShowPoint(int value, Vector3 initPosition, float time, float hight)
        {
            TargetPoints point = MonoBehaviour.Instantiate(_targetPointsPrefab, initPosition, Quaternion.identity);
            point.ShowPoints(value, time, hight);
        }

    }
}   