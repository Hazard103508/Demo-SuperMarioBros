using Mario.Application.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class TimeService : MonoBehaviour, ITimeService
    {
        private float _timer;

        public float TimeSpeed { get; set; }
        public int Time { get; private set; }
        public bool Enabled { get; private set; }

        public UnityEvent OnTimeChanged { get; set; }
        public UnityEvent OnTimeOut { get; set; }


        public void LoadService()
        {
            OnTimeChanged = new UnityEvent();
            OnTimeOut = new UnityEvent();
        }
        private void Update() => UpdateTimer();

        public void ResetTimer()
        {
            TimeSpeed = 2.5f;
            this.Time = AllServices.GameDataService.CurrentMapProfile.Time.StartTime;
            _timer = 0;
        }
        public void StopTimer() => Enabled = false;
        public void StartTimer() => Enabled = true;
        private void UpdateTimer()
        {
            if (Enabled && this.Time > 0)
            {
                _timer += UnityEngine.Time.deltaTime * TimeSpeed;
                this.Time = Mathf.Max(0, AllServices.GameDataService.CurrentMapProfile.Time.StartTime - (int)_timer);
                OnTimeChanged.Invoke();

                if (this.Time == 0)
                    OnTimeOut.Invoke();
            }
        }
    }
}