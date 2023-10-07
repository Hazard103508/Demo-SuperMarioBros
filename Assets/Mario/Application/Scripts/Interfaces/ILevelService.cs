using Mario.Game.ScriptableObjects.Map;
using System;
using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface ILevelService : IGameService
    {
        MapProfile CurrentMapProfile { get; set; }
        MapProfile NextMapProfile { get; set; }
        bool IsGoalReached { get; set; }

        event Action LevelLoaded;
        event Action GoalReached;
        event Action BackScreenEnabled;
        event Action BackScreenDisabled;

        void LoadLevel(Transform parent);
        void UnloadLevel();
    }
}