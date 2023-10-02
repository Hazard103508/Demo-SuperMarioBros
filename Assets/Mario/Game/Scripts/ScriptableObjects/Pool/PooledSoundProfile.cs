using UnityEngine;

namespace Mario.Game.ScriptableObjects.Pool
{
    [CreateAssetMenu(fileName = "PooledSoundProfile", menuName = "ScriptableObjects/Game/Pool/PooledSoundProfile", order = 2)]
    public class PooledSoundProfile : PooledBaseProfile
    {
        public AudioClip Clip;
    }
}