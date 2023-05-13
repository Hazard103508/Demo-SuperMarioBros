using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface ITimeService : IGameService
    {
        bool Enabled { get; }
        UnityEvent OnTimeChanged { get; set; }
        void ResetTimer();
        void StopTimer();
        void StartTimer();
        void UpdateTimer();
    }
}