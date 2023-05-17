using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "FlowerProfile", menuName = "ScriptableObjects/Game/Items/FlowerProfile", order = 2)]
    public class FlowerProfile : ScriptableObject
    {
        public int Points;
    }
}