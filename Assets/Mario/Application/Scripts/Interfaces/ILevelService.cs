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

        event Action LevelLoaded;
        event Action GoalReached;

        void LoadLevel(Transform parent);
        void UnloadLevel();
        void SetNextMap(MapConnection connection);
    }
}