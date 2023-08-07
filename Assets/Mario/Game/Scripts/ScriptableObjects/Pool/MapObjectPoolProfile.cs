using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Pool
{
    [CreateAssetMenu(fileName = "MapObjectPoolProfile", menuName = "ScriptableObjects/Game/Pool/MapObjectPoolProfile", order = 3)]
    public class MapObjectPoolProfile : ScriptableObject
    {
        public ObjectPoolProfile[] ItemsPool;

        public Dictionary<string, ObjectPoolProfile> PoolObjectsDic { get; private set; }

        private void OnEnable()
        {
            PoolObjectsDic = new Dictionary<string, ObjectPoolProfile>();
            foreach (var item in ItemsPool)
                PoolObjectsDic.Add(item.Type, item);
        }

    }
}