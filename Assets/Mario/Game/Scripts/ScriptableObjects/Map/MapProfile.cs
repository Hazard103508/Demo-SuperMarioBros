using Mario.Game.Enums;
using Mario.Game.ScriptableObjects.Pool;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "MapProfile", menuName = "ScriptableObjects/Game/Map/MapProfile", order = 0)]
    public class MapProfile : ScriptableObject
    {
        public string WorldName;
        public MapInit MapInit;
        public MapTime Time;
        public AssetReference[] MapSectionReferences;
        public MapProfile[] PipesConnections;
        public MapProfile[] CheckPoints;
        public MapProfile NextMap;
        public MusicProfile Music;
        public PooledProfileGroup[] PoolProfiles;

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