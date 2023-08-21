using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "MushroomRedProfile", menuName = "ScriptableObjects/Game/Items/MushroomRedProfile", order = 3)]
    public class MushroomRedProfile : MushroomProfile
    {
        public int Points;
    }
}