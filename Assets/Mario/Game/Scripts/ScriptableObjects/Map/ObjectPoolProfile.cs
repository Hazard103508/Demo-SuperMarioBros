using Mario.Game.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "ObjectPoolProfile", menuName = "ScriptableObjects/Game/Map/ObjectPoolProfile", order = 3)]
    public class ObjectPoolProfile : ScriptableObject
    {
        public ObjectPoolItem[] PoolObjects;

        public Dictionary<ObjectPoolTypes, ObjectPoolItem> PoolObjectsDic { get; private set; }

        private void OnEnable()
        {
            PoolObjectsDic = new Dictionary<ObjectPoolTypes, ObjectPoolItem>();
            foreach (var item in PoolObjects)
                PoolObjectsDic.Add(item.Type, item);
        }

    }
    [Serializable]
    public class ObjectPoolItem
    {
        public ObjectPoolTypes Type;
        public AssetReferenceGameObject Reference;
    }
}