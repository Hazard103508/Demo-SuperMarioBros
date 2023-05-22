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
    }
    [Serializable]
    public class MapSection
    {
        public int InitXPosition;
        public int Width;
        public AssetReferenceGameObject Reference;
    }
}