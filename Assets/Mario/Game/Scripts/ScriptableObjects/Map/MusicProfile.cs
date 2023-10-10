using Mario.Game.ScriptableObjects.Pool;
using System;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "MusicProfile", menuName = "ScriptableObjects/Game/Map/MusicProfile", order = 2)]
    public class MusicProfile : ScriptableObject
    {
        public PooledSoundProfile MainTheme;
        public PooledSoundProfile VictoryTheme;
        public PooledSoundProfile HurryFX;
        public HurryTheme HurryTheme;
    }
    [Serializable]
    public class HurryTheme
    {
        public PooledSoundProfile Profile;
        public float StartTimeInit;
        public float StartTimeContinued;
    }
}