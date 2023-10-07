using Mario.Application.Interfaces;
using System;
using UnityEngine;

namespace Mario.Application.Services
{
    public class TimeService : MonoBehaviour, ITimeService
    {
        #region Objects
        private float _timer;
        private bool _isHurry;
        private int _hurryTime = 100;
        #endregion

        #region Properties
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
                    HurryUpTimeStarted?.Invoke();
            }
        }
        #endregion

        #region Events
        public event Action TimeStarted;
        public event Action TimeChangeded;
        public event Action HurryUpTimeStarted;
        public event Action TimeOut;
        #endregion

        #region Unity Methods
        private void Update()
        {
            UpdateTimer();
            ValidHurryUpTime();
        }
        #endregion

        #region Public Methods
        public void LoadService()
        {
            this.StartTime = ServiceLocator.Current.Get<ILevelService>().CurrentMapProfile.Time.StartTime;
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
            TimeStarted?.Invoke();
            Enabled = true;
        }
        #endregion

        #region Private Methods
        private void UpdateTimer()
        {
            if (Enabled && this.Time > 0)
            {
                _timer += UnityEngine.Time.deltaTime * TimeSpeed;
                this.Time = Mathf.Max(0, this.StartTime - (int)_timer);
                TimeChangeded?.Invoke();

                if (this.Time == 0)
                    TimeOut?.Invoke();
            }
        }
        private void ValidHurryUpTime()
        {
            if (Services.GameDataService.CurrentMapProfile.Time.Type == Game.ScriptableObjects.Map.MapTimeType.None)
                return;

            if (!IsHurry && Time <= _hurryTime && !Services.GameDataService.IsGoalReached)
                IsHurry = true;
        }
        #endregion
    }
}