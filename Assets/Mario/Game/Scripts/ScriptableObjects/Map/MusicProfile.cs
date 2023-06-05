using System;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "MusicProfile", menuName = "ScriptableObjects/Game/Map/MusicProfile", order = 2)]
    public class MusicProfile : ScriptableObject
    {
        public MusicTheme MainTheme;
        public MusicTheme VictoryTheme;
        public AudioClip HurryFX;
        public MusicTheme HurryTheme;
        public MusicTheme HurryThemeFirstTime;
    }
    [Serializable]
    public class MusicTheme
    {
        public AudioClip Clip;
        public float StartTime;
    }
}