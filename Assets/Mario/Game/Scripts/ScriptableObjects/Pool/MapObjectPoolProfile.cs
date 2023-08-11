using System.Collections.Generic;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Pool
{
    [CreateAssetMenu(fileName = "MapObjectPoolProfile", menuName = "ScriptableObjects/Game/Pool/MapObjectPoolProfile", order = 1)]
    public class MapObjectPoolProfile : ScriptableObject
    {
        public PooledObjectProfile[] ItemsPool;

        public Dictionary<string, PooledObjectProfile> PoolObjectsDic { get; private set; }

        private void OnEnable()
        {
            PoolObjectsDic = new Dictionary<string, PooledObjectProfile>();
            foreach (var item in ItemsPool)
                PoolObjectsDic.Add(item.name, item);
        }

    }
}