using UnityEngine;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FlowerProfile", menuName = "ScriptableObjects/Game/FlowerProfile", order = 8)]
    public class FlowerProfile : ScriptableObject
    {
        public int Points;
    }
}