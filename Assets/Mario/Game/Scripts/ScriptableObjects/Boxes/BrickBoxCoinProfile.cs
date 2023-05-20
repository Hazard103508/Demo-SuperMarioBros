using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BrickBoxCoinProfile", menuName = "ScriptableObjects/Game/Boxes/BrickBoxCoinProfile", order = 1)]
    public class BrickBoxCoinProfile : ScriptableObject
    {
        public AssetReferenceGameObject CoinReference;
        public float LimitTime;
    }
}