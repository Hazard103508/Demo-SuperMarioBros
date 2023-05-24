using Mario.Application.Interfaces;
using Mario.Game.Enums;
using Mario.Game.ScriptableObjects.Map;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class GameDataService : IGameDataService
    {
        private bool _isMapCompleted;

        public PlayerProfile PlayerProfile { get; set; }
        public MapProfile CurrentMapProfile { get; set; }
        public MapProfile NextMapProfile { get; set; }

        public bool IsMapCompleted
        {
            get => _isMapCompleted;
            set
            {
                _isMapCompleted = value;
                if (value)
                    OnFlagReached.Invoke();
            }
        }

        public UnityEvent OnFlagReached { get; private set; }
        public UnityEvent OnMapCompleted { get; private set; }

        public GameDataService()
        {
            OnFlagReached = new UnityEvent();
            OnMapCompleted = new UnityEvent();
        }
    }
}