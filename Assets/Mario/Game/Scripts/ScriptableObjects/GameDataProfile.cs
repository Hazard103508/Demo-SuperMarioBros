using UnityEngine;
using UnityEngine.Events;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameDataProfile", menuName = "ScriptableObjects/Game/GameDataProfile", order = 99)]
    public class GameDataProfile : ScriptableObject
    {
        [SerializeField] private string _player;
        [SerializeField] private int _score;

        public int Score 
        {
            get => _score;
            set
            {
                _score = value;
                onScoreChanged.Invoke();
            }
        }
        public string Player => _player;

        [HideInInspector] public UnityEvent onScoreChanged;
    }
}