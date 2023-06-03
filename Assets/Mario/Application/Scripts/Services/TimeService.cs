using Mario.Application.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class TimeService : MonoBehaviour, ITimeService
    {
        private float _timer;

        public float TimeSpeed { get; set; }
        public int StartTime { get; set; }
        public int Time { get; private set; }
        public bool Enabled { get; private set; }

        public UnityEvent OnTimeStart { get; set; }
        public UnityEvent OnTimeChanged { get; set; }
        public UnityEvent OnTimeOut { get; set; }


        public void LoadService()
        {
            OnTimeStart = new UnityEvent();
            OnTimeChanged = new UnityEvent();
            OnTimeOut = new UnityEvent();

            this.StartTime = ServiceLocator.Current.Get<IGameDataService>().CurrentMapProfile.Time.StartTime;
        }
        private void Update() => UpdateTimer();

        public void ResetTimer()
        {
            TimeSpeed = 2.5f;
            this.Time = this.StartTime;
            _timer = 0;
        }
        public void StopTimer() => Enabled = false;
        public void StartTimer()
        {
            OnTimeStart.Invoke();
            Enabled = true;
        }
        private void UpdateTimer()
        {
            if (Enabled && this.Time > 0)
            {
                _timer += UnityEngine.Time.deltaTime * TimeSpeed;
                this.Time = Mathf.Max(0, this.StartTime - (int)_timer);
                OnTimeChanged.Invoke();

                if (this.Time == 0)
                    OnTimeOut.Invoke();
            }
        }
    }
}