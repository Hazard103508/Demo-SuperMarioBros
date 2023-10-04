using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Pool
{
    public abstract class PooledBaseProfile : ScriptableObject
    {
        [Header("Pool")]
        public AssetReference Reference;
        public bool CollectionCheck = true;
        public int DefaultCapacity = 1;
        public int MaxSize = 1;
    }
}