using System;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "MapProfile", menuName = "ScriptableObjects/Game/Map/MapProfile", order = 0)]
    public class MapProfile : ScriptableObject
    {
        public string WorldName;
        public MapInit MapInit;
        public MapSounds Sounds;
        public MapTime Time;
        public MapSection[] MapsSections;
        public MapCheckPoint CheckPoint;
        public MapEndPoint EndPoint;
    }
    [Serializable]
    public class MapInit
    {
        public Color BackgroundColor;
        public Vector2 StartPosition;
        public float BlackScreenTime;
    }
    [Serializable]
    public class MapSection
    {
        public int InitXPosition;
        public int Width;
        public GameObject Reference;
    }
    [Serializable]
    public class MapCheckPoint
    {
        public int PositionX;
        public MapProfile mapProfile;
    }
    [Serializable]
    public class MapEndPoint
    {
        public int PositionX;
        public MapProfile mapProfile;
        public int RemainingTimePoints;
        public AudioClip VictoryTheme;
    }
    [Serializable]
    public class MapSounds
    {
        public AudioClip MainTheme;
        public AudioClip HurryTheme;
    }
    [Serializable]
    public class MapTime
    {
        public bool UseTime;
        public int StartTime;
    }
}