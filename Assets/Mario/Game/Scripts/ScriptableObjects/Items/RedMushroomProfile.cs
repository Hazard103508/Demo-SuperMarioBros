using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "RedMushroomProfile", menuName = "ScriptableObjects/Game/Items/RedMushroomProfile", order = 3)]
    public class RedMushroomProfile : ScriptableObject
    {
        public AssetReferenceGameObject PowerUpFXReference;
        public int Points;
    }
}