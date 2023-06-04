using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapTimeHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource _timeScoreFX;
        private int _previousTime;
        private bool isHurry;

        private void Awake()
        {
            AllServices.TimeService.OnTimeChanged.AddListener(OnTimeChanged);
            AllServices.GameDataService.OnGoalReached.AddListener(OnGoalReached);

            if (AllServices.GameDataService.CurrentMapProfile.Time.Type == Game.ScriptableObjects.Map.MapTimeType.None)
                Destroy(this);
        }
        private void OnDestroy()
        {
            AllServices.TimeService.OnTimeChanged.RemoveListener(OnTimeChanged);
            AllServices.GameDataService.OnGoalReached.RemoveListener(OnGoalReached);
        }

        public void OnGoalReached() => AllServices.TimeService.TimeSpeed = 150f;
        public void OnTimeChanged()
        {
            if (AllServices.GameDataService.IsGoalReached)
            {
                int _timedif = _previousTime - AllServices.TimeService.Time;
                AllServices.ScoreService.Add(_timedif * AllServices.GameDataService.CurrentMapProfile.EndPoint.RemainingTimePoints);

                _timeScoreFX.Play();
            }

            if (!isHurry && AllServices.TimeService.Time <= 100 && !AllServices.GameDataService.IsGoalReached)
            {
                isHurry = true;
                AllServices.MusicService.Clip = AllServices.GameDataService.CurrentMapProfile.Time.HurryTheme;
                AllServices.MusicService.Play();
                return;
            }

            _previousTime = AllServices.TimeService.Time;
        }
    }
}