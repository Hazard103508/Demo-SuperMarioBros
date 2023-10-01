using Mario.Game.ScriptableObjects.Map;
using Mario.Game.ScriptableObjects.Player;
using System;

namespace Mario.Application.Interfaces
{
    public interface ILevelService : IGameService
    {
        MapProfile CurrentMapProfile { get; set; }
        MapProfile NextMapProfile { get; set; }

        bool IsGoalReached { get; set; }

        event Action GoalReached;
    }
}