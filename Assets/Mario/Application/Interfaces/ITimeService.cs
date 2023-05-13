using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface ITimeService : IGameService
    {
        bool Enabled { get; set; }
        UnityEvent OnTimeChanged { get; set; }
    }
}