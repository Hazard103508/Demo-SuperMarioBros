using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Map;
using Mario.Game.ScriptableObjects.Player;
using System;
using UnityEngine;

namespace Mario.Application.Services
{
    public class LevelService : MonoBehaviour, ILevelService
    {
        #region Objects
        private bool _isGoalReached;
        #endregion

        #region Properties
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
                    GoalReached?.Invoke();
            }
        }
        #endregion

        #region Events
        public event Action GoalReached;
        #endregion

        #region Public Methods
        public void LoadService()
        {
        }
        #endregion
    }
}