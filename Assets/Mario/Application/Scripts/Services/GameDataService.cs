using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Map;
using Mario.Game.ScriptableObjects.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class GameDataService : MonoBehaviour, IGameDataService
    {
        private bool _isGoalReached;

        [field: SerializeField] public PlayerProfile PlayerProfile { get; set; }
        [field: SerializeField] public MapProfile CurrentMapProfile { get; set; }
        public MapProfile NextMapProfile { get; set; }

        public bool IsGoalReached
        {
            get => _isGoalReached;
            set
            {
                _isGoalReached = value;
                if (value)
                    OnGoalReached.Invoke();
            }
        }

        public UnityEvent OnGoalReached { get; private set; }

        public void LoadService()
        {
            OnGoalReached = new UnityEvent();
        }
    }
}