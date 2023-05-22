using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "FlowerProfile", menuName = "ScriptableObjects/Game/Items/FlowerProfile", order = 1)]
    public class FlowerProfile : ScriptableObject
    {
        public AssetReferenceGameObject PowerUpFXReference;
        public int Points;
    }
}