using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "MapProfile", menuName = "ScriptableObjects/Game/Map/MapProfile", order = 0)]
    public class MapProfile : ScriptableObject
    {
        public string WorldName;
        public Color BackgroundColor;
        public int Time;
        public Vector2 StartPosition;
        public MapSection[] MapsSections;
        public NewMapPoint CheckPoint;
        public NewMapPoint WinPoint;
        public MapSounds Sounds;
        public int RemainingTimePoints;
    }
    [Serializable]
    public class MapSection
    {
        public int InitXPosition;
        public int Width;
        public AssetReferenceGameObject Reference;
    }
    [Serializable]
    public class NewMapPoint
    {
        public int PositionX;
        public MapProfile mapProfile;
    }
    [Serializable]
    public class MapSounds
    {
        public AudioClip MainTheme;
        public AudioClip HurryTheme;
        public AudioClip VictoryTheme;
    }
}