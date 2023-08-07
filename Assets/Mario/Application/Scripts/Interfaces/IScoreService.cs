using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface IScoreService : IGameService
    {
        int Score { get; }

        UnityEvent OnScoreChanged { get; set; }
        void Add(int points);
        void ShowPoint(int value, Vector3 initPosition, float time, float hight, bool deactivateOnCompleted);
        void ShowPoints(int points, Vector3 initPosition, float time, float hight);
        void Show1UP(Vector3 initPosition, float time, float hight);
        void Reset();
    }
}