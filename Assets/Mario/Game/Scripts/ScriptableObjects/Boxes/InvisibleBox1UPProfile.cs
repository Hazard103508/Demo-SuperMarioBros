using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "InvisibleBox1UPProfile", menuName = "ScriptableObjects/Game/Boxes/InvisibleBox1UPProfile", order = 1)]
    public class InvisibleBox1UPProfile : ScriptableObject
    {
        public AssetReferenceGameObject GreenMushroomReference;
    }
}