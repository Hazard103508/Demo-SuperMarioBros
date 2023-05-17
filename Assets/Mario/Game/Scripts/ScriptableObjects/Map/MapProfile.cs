using UnityEngine;

namespace Mario.Game.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "MapProfile", menuName = "ScriptableObjects/Game/MapProfile", order = 7)]
    public class MapProfile : ScriptableObject
    {
        public string WorldName;
        public Color BackgroundColor;
        public int Time;
        public Vector2 StartPosition;
    }
}