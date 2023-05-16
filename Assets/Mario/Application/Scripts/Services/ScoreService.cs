using Mario.Application.Interfaces;
using Mario.Game.Environment;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class ScoreService : IScoreService
    {
        private TargetPoints _targetPointsPrefab;

        public int Score { get; private set; }


        public ScoreService(TargetPoints targetPointsPrefab)
        {
            _targetPointsPrefab = targetPointsPrefab;
            OnScoreChanged = new UnityEvent();
        }

        public UnityEvent OnScoreChanged { get; set; }

        public void Add(int points)
        {
            this.Score += points;
            OnScoreChanged.Invoke();
        }
        public void ShowPoint(int value, Vector3 initPosition, float time, float hight)
        {
            TargetPoints point = MonoBehaviour.Instantiate(_targetPointsPrefab, initPosition, Quaternion.identity);
            point.ShowPoints(value, time, hight);
        }

    }
}