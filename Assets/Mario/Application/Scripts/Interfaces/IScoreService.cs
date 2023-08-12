using System;
using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface IScoreService : IGameService
    {
        int Score { get; }

        event Action ScoreChanged;

        void Add(int points);
        void ShowPoints(int points, Vector3 initPosition, float time, float hight);
        void Show1UP(Vector3 initPosition, float time, float hight);
        void Reset();
    }
}