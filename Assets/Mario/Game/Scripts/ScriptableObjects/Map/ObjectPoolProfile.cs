using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "ObjectPoolProfile", menuName = "ScriptableObjects/Game/Map/ObjectPoolProfile", order = 3)]
    public class ObjectPoolProfile : ScriptableObject
    {
        public ObjectPoolItem[] ItemsPool;

        public Dictionary<string, ObjectPoolItem> PoolObjectsDic { get; private set; }

        private void OnEnable()
        {
            PoolObjectsDic = new Dictionary<string, ObjectPoolItem>();
            foreach (var item in ItemsPool)
                PoolObjectsDic.Add(item.Type, item);
        }

    }
    [Serializable]
    public class ObjectPoolItem
    {
        public string Type;
        public bool RequireCanvas;
        public AssetReferenceGameObject Reference;
    }
}