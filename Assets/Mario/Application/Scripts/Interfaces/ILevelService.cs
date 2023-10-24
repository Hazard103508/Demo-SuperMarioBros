using Mario.Game.ScriptableObjects.Map;
using System;
using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface ILevelService : IGameService
    {
        MapProfile MapProfile { get; }
        bool IsGoalReached { get; set; }
        bool IsLoadCompleted { get; }

        event Action GoalReached;

        void LoadLevel();
        void LoadNextLevel();
        void UnloadLevel();
        void SetNextMap(MapConnection connection);
    }
}