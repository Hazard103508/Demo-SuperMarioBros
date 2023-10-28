using UnityEngine;

namespace Mario.Game.ScriptableObjects.Pool
{
    [CreateAssetMenu(fileName = "PooledProfileGroup", menuName = "ScriptableObjects/Game/Pool/PooledProfileGroup", order = 3)]
    public class PooledProfileGroup : ScriptableObject
    {
        [SerializeField] private PooledObjectProfile[] _pooledObjectProfiles;
        [SerializeField] private PooledSoundProfile[] _pooledSoundProfiles;
        [SerializeField] private PooledUIProfile[] _pooledUIProfiles;

        public PooledObjectProfile[] PooledObjectProfiles => _pooledObjectProfiles;
        public PooledSoundProfile[] PooledSoundProfiles => _pooledSoundProfiles;
        public PooledUIProfile[] PooledUIProfiles => _pooledUIProfiles;
    }
}