using Mario.Game.ScriptableObjects.Pool;
using System;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "MusicProfile", menuName = "ScriptableObjects/Game/Map/MusicProfile", order = 2)]
    public class MusicProfile : ScriptableObject
    {
        [SerializeField] private PooledSoundProfile _mainTheme;
        [SerializeField] private PooledSoundProfile _victoryTheme;
        [SerializeField] private PooledSoundProfile _starman;
        [SerializeField] private PooledSoundProfile _starmanHurry;
        [SerializeField] private PooledSoundProfile _hurryFX;
        [SerializeField] private HurryTheme _hurryTheme;

        public PooledSoundProfile MainTheme => _mainTheme;
        public PooledSoundProfile VictoryTheme => _victoryTheme;
        public PooledSoundProfile Starman => _starman;
        public PooledSoundProfile StarmanHurry => _starmanHurry;
        public PooledSoundProfile HurryFX => _hurryFX;
        public HurryTheme HurryTheme => _hurryTheme;
    }
    [Serializable]
    public class HurryTheme
    {
        [SerializeField] private PooledSoundProfile _profile;
        [SerializeField] private float _startTimeInit;
        [SerializeField] private float _startTimeContinued;

        public PooledSoundProfile Profile => _profile;
        public float StartTimeInit => _startTimeInit;
        public float StartTimeContinued => _startTimeContinued;
    }
}