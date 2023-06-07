using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Map;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapTimeScore : MonoBehaviour
    {
        [SerializeField] private AudioSource _timeScoreFX;
        private int _previousTime;
        private int _pointsPerSecond = 50;

        private void Awake()
        {
            AllServices.TimeService.OnTimeChanged.AddListener(OnTimeChanged);
            AllServices.GameDataService.OnGoalReached.AddListener(OnGoalReached);

            if (AllServices.GameDataService.CurrentMapProfile.Time.Type == MapTimeType.None)
                Destroy(this);
        }

        private void OnDestroy()
        {
            AllServices.TimeService.OnTimeChanged.RemoveListener(OnTimeChanged);
            AllServices.GameDataService.OnGoalReached.RemoveListener(OnGoalReached);
        }

        public void OnGoalReached() => AllServices.TimeService.TimeSpeed = 150f;
        public void OnTimeChanged() => ValidScoreCount();

        private void ValidScoreCount()
        {
            if (AllServices.GameDataService.IsGoalReached)
            {
                int _timedif = _previousTime - AllServices.TimeService.Time;
                AllServices.ScoreService.Add(_timedif * _pointsPerSecond);

                _timeScoreFX.Play();
            }
            _previousTime = AllServices.TimeService.Time;
        }
    }
}