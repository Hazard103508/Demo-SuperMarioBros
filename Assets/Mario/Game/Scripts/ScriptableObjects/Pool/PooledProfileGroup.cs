using UnityEngine;

namespace Mario.Game.ScriptableObjects.Pool
{
    [CreateAssetMenu(fileName = "PooledProfileGroup", menuName = "ScriptableObjects/Game/Pool/PooledProfileGroup", order = 3)]
    public class PooledProfileGroup : ScriptableObject
    {
        public PooledObjectProfile[] PooledObjectProfiles;
        public PooledSoundProfile[] PooledSoundProfiles;
        public PooledUIProfile[] PooledUIProfiles;
    }
}