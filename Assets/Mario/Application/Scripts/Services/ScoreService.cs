using Mario.Application.Interfaces;
using Mario.Game.Environment;
using Mario.Game.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class ScoreService : MonoBehaviour, IScoreService
    {
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
        public void ShowPoint(int value, Vector3 initPosition, float time, float hight, bool deactivateOnCompleted)
        {
            TargetPoints point = Services.PoolService.GetObjectFromPool<TargetPoints>("TargetPoints");
            point.transform.position = initPosition;
            point.gameObject.SetActive(true);

            point.ShowPoints(value, time, hight, deactivateOnCompleted);
        }
        public void ShowPoints(int points, Vector3 initPosition, float time, float hight)
        {
            var label = Services.PoolService.GetObjectFromPool<WorldLabel>("WorldLabel");
            label.transform.position = initPosition;
            label.Show(points.ToString().PadLeft(4), time, hight);
        }
        public void Show1UP(Vector3 initPosition, float time, float hight)
        {
            var label = Services.PoolService.GetObjectFromPool<WorldLabel>("WorldLabel");
            label.transform.position = initPosition;
            label.Show("+", time, hight);
        }

        public void Reset()
        {
            Score = 0;
        }

    }
}