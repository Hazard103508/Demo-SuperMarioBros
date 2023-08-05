using Mario.Game.Enums;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "ObjectPoolProfile", menuName = "ScriptableObjects/Game/Map/ObjectPoolProfile", order = 3)]
    public class ObjectPoolProfile : ScriptableObject
    {
        public ObjectPoolItem[] PoolObjects;
    }
    [Serializable]
    public class ObjectPoolItem
    {
        public ObjectPoolTypes Type;
        public AssetReferenceGameObject Reference;
    }
}