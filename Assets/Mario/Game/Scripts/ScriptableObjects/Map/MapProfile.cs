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
        [SerializeField] private string _worldName;
        [SerializeField] private MapInit _mapInit;
        [SerializeField] private MapTime _time;
        [SerializeField] private AssetReference[] _mapSectionReferences;
        [SerializeField] private MapProfile[] _pipesConnections;
        [SerializeField] private MapProfile[] _checkPoints;
        [SerializeField] private MapProfile _nextMap;
        [SerializeField] private MusicProfile _music;
        [SerializeField] private PooledProfileGroup[] _poolProfiles;

        public string WorldName => _worldName;
        public MapInit MapInit => _mapInit;
        public MapTime Time => _time;
        public AssetReference[] MapSectionReferences => _mapSectionReferences;
        public MapProfile[] PipesConnections => _pipesConnections;
        public MapProfile[] CheckPoints => _checkPoints;
        public MapProfile NextMap => _nextMap;
        public MusicProfile Music => _music;
        public PooledProfileGroup[] PoolProfiles => _poolProfiles;
        public int Width { get; set; }
    }
    [Serializable]
    public class MapInit
    {
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private PlayerStartLocation _startLocation;
        [SerializeField] private float _blackScreenTime;

        public Color BackgroundColor => _backgroundColor;
        public Vector2 StartPosition => _startPosition;
        public PlayerStartLocation StartLocation => _startLocation;
        public float BlackScreenTime => _blackScreenTime;
    }
    [Serializable]
    public class MapTime
    {
        [SerializeField] private MapTimeType _type;
        [SerializeField] private int _startTime;

        public MapTimeType Type => _type;
        public int StartTime => _startTime;
    }
    public enum MapTimeType
    {
        None,
        Beginning,
        Continuated
    }
}