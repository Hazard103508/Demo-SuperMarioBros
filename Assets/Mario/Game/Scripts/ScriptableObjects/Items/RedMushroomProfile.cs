using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "RedMushroomProfile", menuName = "ScriptableObjects/Game/Items/RedMushroomProfile", order = 3)]
    public class RedMushroomProfile : ScriptableObject
    {
        public int Points;
    }
}