using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Pool
{
    public abstract class PooledBaseProfile : ScriptableObject
    {
        [Header("Pool")]
        [SerializeField] private AssetReference _reference;
        [SerializeField] private int _defaultCapacity = 1;
        [SerializeField] private int _maxSize = 1;

        public AssetReference Reference => _reference;
        public int DefaultCapacity => _defaultCapacity;
        public int MaxSize => _maxSize;
    }
}