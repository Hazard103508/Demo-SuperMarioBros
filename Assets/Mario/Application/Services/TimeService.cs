using Mario.Application.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class TimeService : ITimeService
    {
        private float _timer;
        private int _initTime;

        public TimeService()
        {
            OnTimeChanged = new UnityEvent();
            Enabled = true;
        }

        public bool Enabled { get; set; }
        public UnityEvent OnTimeChanged { get; set; }

        public void Reset()
        {
            _initTime = AllServices.GameDataService.MapProfile.Time;
            _timer = 0;
        }
        public void Update()
        {
            if (Enabled)
            {
                _timer += Time.deltaTime * 2.5f;
                AllServices.GameDataService.Time = _initTime - (int)_timer;
                OnTimeChanged.Invoke();
            }
        }
    }
}