using Mario.Application.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class TimeService : ITimeService
    {
        private float _timer;

        public TimeService()
        {
            OnTimeChanged = new UnityEvent();
        }

        public bool Enabled { get; private set; }
        public UnityEvent OnTimeChanged { get; set; }

        public void ResetTimer()
        {
            _timer = 0;
        }
        public void StopTimer() => Enabled = false;
        public void StartTimer() => Enabled = true;
        public void UpdateTimer()
        {
            if (Enabled)
            {
                _timer += Time.deltaTime * 2.5f;
                AllServices.GameDataService.Time = AllServices.GameDataService.MapProfile.Time - (int)_timer;
                OnTimeChanged.Invoke();
            }
        }
    }
}