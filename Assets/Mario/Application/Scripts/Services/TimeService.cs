using Mario.Application.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class TimeService : MonoBehaviour, ITimeService
    {
        private float _timer;
        private bool _isHurry;
        private int _hurryTime = 100;

        public float TimeSpeed { get; set; }
        public int StartTime { get; set; }
        public int Time { get; private set; }
        public bool Enabled { get; private set; }
        public bool IsHurry
        {
            get => _isHurry;
            private set
            {
                _isHurry = value;
                if (value)
                    OnHurryUpTimeStart.Invoke();
            }
        }

        public UnityEvent OnTimeStart { get; private set; }
        public UnityEvent OnTimeChanged { get; private set; }
        public UnityEvent OnHurryUpTimeStart { get; private set; }
        public UnityEvent OnTimeOut { get; private set; }


        public void LoadService()
        {
            OnTimeStart = new UnityEvent();
            OnTimeChanged = new UnityEvent();
            OnHurryUpTimeStart = new UnityEvent();
            OnTimeOut = new UnityEvent();

            this.StartTime = ServiceLocator.Current.Get<IGameDataService>().CurrentMapProfile.Time.StartTime;
        }
        private void Update()
        {
            UpdateTimer();
            ValidHurryUpTime();
        }

        public void ResetTimer()
        {
            TimeSpeed = 2.5f;
            this.Time = this.StartTime;
            _timer = 0;
            IsHurry = false;
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
        private void ValidHurryUpTime()
        {
            if (Services.GameDataService.CurrentMapProfile.Time.Type == Game.ScriptableObjects.Map.MapTimeType.None)
                return;

            if (!IsHurry && Services.TimeService.Time <= _hurryTime && !Services.GameDataService.IsGoalReached)
                IsHurry = true;
        }
    }
}