using System.Collections.Generic;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Pool
{
    [CreateAssetMenu(fileName = "ScenesPoolProfile", menuName = "ScriptableObjects/Game/Pool/ScenesPoolProfile", order = 3)]
    public class ScenesPoolProfile : ScriptableObject
    {
        public PooledObjectProfile[] ObjectPool;
        public PooledSoundProfile[] SoundPool;
        public PooledUIProfile[] UIPool;

        public Dictionary<string, PooledObjectProfile> PoolObjectsDic { get; private set; }
        public Dictionary<string, PooledSoundProfile> PoolSoundDic { get; private set; }
        public Dictionary<string, PooledUIProfile> PooledUIDic { get; private set; }

        private void OnEnable()
        {
            PoolObjectsDic = new Dictionary<string, PooledObjectProfile>();
            foreach (var item in ObjectPool)
                PoolObjectsDic.Add(item.name, item);

            PoolSoundDic = new Dictionary<string, PooledSoundProfile>();
            foreach (var item in SoundPool)
                PoolSoundDic.Add(item.name, item);

            PooledUIDic = new Dictionary<string, PooledUIProfile>();
            foreach (var item in UIPool)
                PooledUIDic.Add(item.name, item);
        }
    }
}