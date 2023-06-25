using Mario.Application.Interfaces;
using Mario.Game.Environment;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class ScoreService : MonoBehaviour, IScoreService
    {
        [SerializeField] private TargetPoints _targetPointsPrefab;

        public int Score { get; private set; }

        public UnityEvent OnScoreChanged { get; set; }

        public void LoadService()
        {
            OnScoreChanged = new UnityEvent();
        }
        public void Add(int points)
        {
            this.Score += points;
            OnScoreChanged.Invoke();

        }
        public void ShowPoint(int value, Vector3 initPosition, float time, float hight) => ShowPoint(value, initPosition, time, hight, true);
        public void ShowPoint(int value, Vector3 initPosition, float time, float hight, bool destroyOnCompleted)
        {
            TargetPoints point = MonoBehaviour.Instantiate(_targetPointsPrefab, initPosition, Quaternion.identity);
            point.ShowPoints(value, time, hight, destroyOnCompleted);
        }
        public void ShowLabel(Sprite label, Vector3 initPosition, float time, float hight)
        {
            TargetPoints point = MonoBehaviour.Instantiate(_targetPointsPrefab, initPosition, Quaternion.identity);
            point.ShowLabel(label, time, hight, true);
        }
    }
}