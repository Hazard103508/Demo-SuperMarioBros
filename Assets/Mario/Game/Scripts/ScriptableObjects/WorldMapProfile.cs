using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WorldMapProfile", menuName = "ScriptableObjects/Game/WorldMapProfile", order = 7)]
    public class WorldMapProfile : ScriptableObject
    {
        public string WorldName;
        public Color BackgroundColor;
        public int Time;
    }
}