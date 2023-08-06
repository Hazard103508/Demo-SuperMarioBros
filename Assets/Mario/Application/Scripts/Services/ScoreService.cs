using Mario.Application.Interfaces;
using Mario.Game.Environment;
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

        public void Reset()
        {
            Score = 0;
        }
    }
}