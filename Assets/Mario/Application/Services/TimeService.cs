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
            Enabled = true;
        }

        public bool Enabled { get; set; }
        public int InitTime { get; set; }
        public UnityEvent OnTimeChanged { get; set; }

        public void Update()
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