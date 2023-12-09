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
        [SerializeField] private int _width;
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private int _startTime;
        [SerializeField] private bool _autowalk;
        [SerializeField] private MusicProfile _music;
        [SerializeField] private AssetReference _mapReferences;
        [SerializeField] private PooledProfileGroup[] _poolProfiles;

        public string WorldName => _worldName;
        public int Width => _width;
        public Color BackgroundColor => _backgroundColor;
        public Vector2 StartPosition => _startPosition;
        public int StartTime => _startTime;
        public bool Autowalk => _autowalk;
        public MusicProfile Music => _music;
        public AssetReference MapReferences => _mapReferences;
        public PooledProfileGroup[] PoolProfiles => _poolProfiles;
    }
    [Serializable]
    public class MapConnection
    {
        [SerializeField] private MapProfile _mapProfile;
        [SerializeField] private PlayerStartMode _startMode;
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private float _cameraPositionX;

        public MapProfile MapProfile => _mapProfile;
        public PlayerStartMode StartMode => _startMode;
        public Vector2 StartPosition => _startPosition;
        public float CameraPositionX => _cameraPositionX;
    }
}