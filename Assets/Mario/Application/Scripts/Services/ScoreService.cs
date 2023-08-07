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
        public void ShowPoint(int value, Vector3 initPosition, float time, float hight) => ShowPoint(value, initPosition, time, hight, true);
        public void ShowPoint(int value, Vector3 initPosition, float time, float hight, bool deactivateOnCompleted)
        {
            TargetPoints point = Services.PoolService.GetObjectFromPool<TargetPoints>("TargetPoints");
            point.transform.position = initPosition;
            point.gameObject.SetActive(true);

            point.ShowPoints(value, time, hight, deactivateOnCompleted);
        }
        public void ShowLabel(Sprite label, Vector3 initPosition, float time, float hight)
        {
            TargetPoints point = Services.PoolService.GetObjectFromPool<TargetPoints>("TargetPoints");
            point.transform.position = initPosition;
            point.gameObject.SetActive(true);

            point.ShowLabel(label, time, hight, true);
        }
        public void ShowPoints(int points, Vector3 initPosition, float time, float hight)
        {
            var label = Services.PoolService.GetObjectFromPool<WorldLabel>("WorldLabel");
            //label.transform.position = Camera.main.WorldToScreenPoint(initPosition);
            label.transform.position = initPosition;
            label.Show(points.ToString().PadLeft(4), time, hight);
            //label.gameObject.SetActive(true);
            //label.Text = points.ToString().PadLeft(4);
            //point.ShowLabel(label, time, hight, true);
        }

        public void Reset()
        {
            Score = 0;
        }
    }
}