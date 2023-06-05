using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface ITimeService : IGameService
    {
        float TimeSpeed { get; set; }
        int StartTime { get; set; }
        int Time { get; }
        bool Enabled { get; }
        bool IsHurry { get; }

        UnityEvent OnTimeStart { get; }
        UnityEvent OnTimeChanged { get; }
        UnityEvent OnHurryUpTimeStart { get; }
        UnityEvent OnTimeOut { get; }

        void ResetTimer();
        void StopTimer();
        void StartTimer();
    }
}