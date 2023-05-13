using UnityEngine;
using UnityEngine.Events;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameDataProfile", menuName = "ScriptableObjects/Game/GameDataProfile", order = 99)]
    public class GameDataProfile : ScriptableObject
    {
        public WorldMapProfile WorldMapProfile;
        [SerializeField] private string _player;

        private int _score;
        private int _timer;

        public void Init()
        {
            Score = 0;
            Timer = WorldMapProfile.Time;
        }
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                onScoreChanged.Invoke();
            }
        }

        public int Timer
        {
            get => _timer;
            set
            {
                float _old = _timer;
                _timer = value;

                if (_timer != _old)
                    onTimeChanged.Invoke();
            }
        }

        public string Player => _player;


        [HideInInspector] public UnityEvent onScoreChanged;
        [HideInInspector] public UnityEvent onTimeChanged;
    }
}