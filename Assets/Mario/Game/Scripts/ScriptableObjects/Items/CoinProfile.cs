using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "CoinProfile", menuName = "ScriptableObjects/Game/Items/CoinProfile", order = 0)]
    public class CoinProfile : ScriptableObject
    {
        public AssetReferenceGameObject CoinSoundFXReference;
        public int Points;
    }
}