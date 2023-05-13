using Mario.Application.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class TimeService : ITimeService
    {
        private IGameDataService _gameDataService;
        private float _timer;

        public TimeService()
        {
            _gameDataService = ServiceLocator.Current.Get<IGameDataService>();
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
                _gameDataService.Time = ServiceLocator.Current.Get<IGameDataService>().MapProfile.Time - (int)_timer;
                OnTimeChanged.Invoke();
            }
        }
    }
}