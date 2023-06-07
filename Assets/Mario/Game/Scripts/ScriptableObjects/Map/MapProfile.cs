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
        public GameObject[] MapSectionReferences;
        public MapProfile[] PipesConnections;
        public MapProfile[] CheckPoints;
        public MapProfile NextMap;
        public MusicProfile Music;
        
        [NonSerialized] public int Width;
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