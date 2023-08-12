using System;

namespace Mario.Application.Interfaces
{
    public interface ITimeService : IGameService
    {
        float TimeSpeed { get; set; }
        int StartTime { get; set; }
        int Time { get; }
        bool Enabled { get; }
        bool IsHurry { get; }

        event Action TimeStarted;
        event Action TimeChangeded;
        event Action HurryUpTimeStarted;
        event Action TimeOut;

        void ResetTimer();
        void StopTimer();
        void StartTimer();
    }
}