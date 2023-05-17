using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "FlowerProfile", menuName = "ScriptableObjects/Game/FlowerProfile", order = 8)]
    public class FlowerProfile : ScriptableObject
    {
        public int Points;
    }
}