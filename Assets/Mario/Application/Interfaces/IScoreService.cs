using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface IScoreService : IGameService
    {
        UnityEvent OnScoreChanged { get; set; }
        void Add(int points);
    }
}