using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Map;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class GameDataService : MonoBehaviour, IGameDataService
    {
        private bool _isMapCompleted;

        [field: SerializeField] public PlayerProfile PlayerProfile { get; set; }
        [field: SerializeField] public MapProfile CurrentMapProfile { get; set; }
        public MapProfile NextMapProfile { get; set; }

        public bool IsMapCompleted
        {
            get => _isMapCompleted;
            set
            {
                _isMapCompleted = value;
                //if (value)
                //    OnMapCompleted.Invoke();
            }
        }

        public UnityEvent OnMapCompleted { get; private set; }

        public void LoadService()
        {
            OnMapCompleted = new UnityEvent();
        }
    }
}