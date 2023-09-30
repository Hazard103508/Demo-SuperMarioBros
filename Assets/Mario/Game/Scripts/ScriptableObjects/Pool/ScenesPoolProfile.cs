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

        public Dictionary<string, BasePooledObjectProfile> WorldPoolProfiles { get; private set; }
        public Dictionary<string, PooledUIProfile> UIPoolProfiles { get; private set; }

        private void OnEnable()
        {
            WorldPoolProfiles = new Dictionary<string, BasePooledObjectProfile>();
            foreach (var item in ObjectPool)
                WorldPoolProfiles.Add(item.name, item);
            foreach (var item in SoundPool)
                WorldPoolProfiles.Add(item.name, item);

            UIPoolProfiles = new Dictionary<string, PooledUIProfile>();
            foreach (var item in UIPool)
                UIPoolProfiles.Add(item.name, item);
        }
    }
}