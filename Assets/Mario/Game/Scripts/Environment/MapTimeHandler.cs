using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapTimeHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource _timeScoreFX;
        private int _previousTime;

        private void Awake()
        {
            AllServices.TimeService.ResetTimer();
            AllServices.TimeService.StartTimer();

            AllServices.TimeService.OnTimeChanged.AddListener(OnTimeChanged);
            AllServices.GameDataService.OnMapCompleted.AddListener(OnMapCompleted);

            if (!AllServices.GameDataService.CurrentMapProfile.Time.UseTime)
                Destroy(this);
        }
        private void OnDestroy()
        {
            AllServices.TimeService.OnTimeChanged.RemoveListener(OnTimeChanged);
            AllServices.GameDataService.OnMapCompleted.RemoveListener(OnMapCompleted);
        }
        private void Update() => AllServices.TimeService.UpdateTimer();

        public void OnMapCompleted() => AllServices.TimeService.TimeSpeed = 150f;
        public void OnTimeChanged()
        {
            if (AllServices.GameDataService.IsMapCompleted)
            {
                int _timedif = _previousTime - AllServices.TimeService.Time;
                AllServices.ScoreService.Add(_timedif * AllServices.GameDataService.CurrentMapProfile.RemainingTimePoints);

                _timeScoreFX.Play();
            }

            _previousTime = AllServices.TimeService.Time;
        }
    }
}