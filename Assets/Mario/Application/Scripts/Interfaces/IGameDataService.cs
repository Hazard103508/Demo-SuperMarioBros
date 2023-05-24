using Mario.Game.Enums;
using Mario.Game.ScriptableObjects.Map;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface IGameDataService : IGameService
    {
        PlayerProfile PlayerProfile { get; set; }
        MapProfile CurrentMapProfile { get; set; }
        MapProfile NextMapProfile { get; set; }

        bool IsMapCompleted { get; set; }

        UnityEvent OnFlagReached { get;}
        UnityEvent OnMapCompleted { get; }
    }
}