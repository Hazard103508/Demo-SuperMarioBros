using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface ITimeService : IGameService
    {
        float TimeSpeed { get; set; }
        int StartTime { get; set; }
        int Time { get; }
        bool Enabled { get; }

        UnityEvent OnTimeChanged { get; set; }
        UnityEvent OnTimeOut { get; set; }

        void ResetTimer();
        void StopTimer();
        void StartTimer();
    }
}