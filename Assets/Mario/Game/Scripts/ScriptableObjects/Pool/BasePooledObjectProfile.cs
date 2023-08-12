using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Pool
{
    public class BasePooledObjectProfile : ScriptableObject
    {
        public AssetReferenceGameObject Reference;
        public bool CollectionCheck = true;
        public int DefaultCapacity = 1;
        public int MaxSize = 1;
    }
}