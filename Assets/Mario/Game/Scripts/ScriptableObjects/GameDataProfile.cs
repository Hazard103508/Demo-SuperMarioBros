using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameDataProfile", menuName = "ScriptableObjects/Game/GameDataProfile", order = 99)]
    public class GameDataProfile : ScriptableObject
    {
        [SerializeField] private string _player;
        [SerializeField] private string _worldName;
        [SerializeField] private int _score;
        [SerializeField] private int _coins;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                onScoreChanged.Invoke();
            }
        }
        public int Coins
        {
            get => _coins;
            set
            {
                _coins = value;
                onCoinsChanged.Invoke();
            }
        }
        public string Player => _player;
        public string WorldName => _worldName;

        [HideInInspector] public UnityEvent onScoreChanged;
        [HideInInspector] public UnityEvent onCoinsChanged;
    }
}