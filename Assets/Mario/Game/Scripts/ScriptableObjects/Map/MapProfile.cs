using System;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "MapProfile", menuName = "ScriptableObjects/Game/Map/MapProfile", order = 0)]
    public class MapProfile : ScriptableObject
    {
        public string WorldName;
        public MapInit MapInit;
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
        public AudioClip MainTheme;
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
        public bool HasCheckPoint;
        public int PositionX;
        public MapProfile mapProfile;
    }
    [Serializable]
    public class MapEndPoint
    {
        public bool HasEndPoint;
        public int PositionX;
        public MapProfile mapProfile;
        public int RemainingTimePoints;
        public AudioClip VictoryTheme;
    }
    [Serializable]
    public class MapTime
    {
        public MapTimeType Type;
        public int StartTime;
        public AudioClip HurryTheme;
    }
    public enum MapTimeType
    {
        None,
        Beginning,
        Continuated
    }
}