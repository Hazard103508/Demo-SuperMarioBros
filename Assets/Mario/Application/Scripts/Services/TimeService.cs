using Mario.Application.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class TimeService : ITimeService
    {
        private float _timer;

        public int Time { get; private set; }
        public bool Enabled { get; private set; }

        public TimeService()
        {
            OnTimeChanged = new UnityEvent();
            OnTimeOut = new UnityEvent();
        }


        public UnityEvent OnTimeChanged { get; set; }
        public UnityEvent OnTimeOut { get; set; }

        public void ResetTimer()
        {
            this.Time = AllServices.GameDataService.CurrentMapProfile.Time;
            _timer = 0;
        }
        public void StopTimer() => Enabled = false;
        public void StartTimer() => Enabled = true;
        public void UpdateTimer()
        {
            if (Enabled && this.Time > 0)
            {
                _timer += UnityEngine.Time.deltaTime * 2.5f;
                this.Time = Mathf.Max(0, AllServices.GameDataService.CurrentMapProfile.Time - (int)_timer);
                OnTimeChanged.Invoke();

                if (this.Time == 0)
                    OnTimeOut.Invoke();
            }
        }
    }
}