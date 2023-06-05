using Mario.Game.Enums;
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
        public MapProfile[] PipesConnections;
        public MapCheckPoint CheckPoint;
        public MapEndPoint EndPoint;
        public MusicProfile Music;
    }
    [Serializable]
    public class MapInit
    {
        public Color BackgroundColor;
        public Vector2 StartPosition;
        public PlayerStartLocation StartLocation;
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
        public bool HasCheckPoint;
        public int PositionX;
        public MapProfile mapProfile;
    }
    [Serializable]
    public class MapEndPoint
    {
        public bool HasEndPoint;
        public MapProfile mapProfile;
        public int RemainingTimePoints;
    }
    [Serializable]
    public class MapTime
    {
        public MapTimeType Type;
        public int StartTime;
    }
    public enum MapTimeType
    {
        None,
        Beginning,
        Continuated
    }
}