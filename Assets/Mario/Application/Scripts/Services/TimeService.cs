using Mario.Application.Interfaces;
using System;
using UnityEngine;

namespace Mario.Application.Services
{
    public class TimeService : MonoBehaviour, ITimeService
    {
        #region Objects
        private float _timer;
        #endregion

        #region Properties
        public float TimeSpeed { get; set; }
        public int StartTime { get; set; }
        public int Time { get; private set; }
        public bool Enabled { get; private set; }
        #endregion

        #region Events
        public event Action TimeStarted;
        public event Action TimeChangeded;
        public event Action TimeOut;
        #endregion

        #region Unity Methods
        private void Update()
        {
            UpdateTimer();
        }
        #endregion

        #region Public Methods
        public void Initalize()
        {
        }
        public void Dispose()
        {
        }
        public void ResetTimer()
        {
            TimeSpeed = 2.5f;
            Time = StartTime;
            _timer = 0;
        }
        public void StartTimer()
        {
            TimeStarted?.Invoke();
            Enabled = true;
        }
        public void FreezeTimer() => Enabled = false;
        public void UnfreezeTimer() => Enabled = true;
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
        #endregion
    }
}