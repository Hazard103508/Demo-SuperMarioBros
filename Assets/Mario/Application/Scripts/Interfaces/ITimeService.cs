using System;

namespace Mario.Application.Interfaces
{
    public interface ITimeService : IGameService
    {
        float TimeSpeed { get; set; }
        int StartTime { get; set; }
        int Time { get; }
        bool Enabled { get; }

        event Action TimeStarted;
        event Action TimeChangeded;
        event Action TimeOut;

        void ResetTimer();
        void StartTimer();
        void FreezeTimer();
        void UnfreezeTimer();
    }
}