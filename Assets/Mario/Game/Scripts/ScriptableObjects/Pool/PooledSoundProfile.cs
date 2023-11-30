using UnityEngine;

namespace Mario.Game.ScriptableObjects.Pool
{
    [CreateAssetMenu(fileName = "PooledSoundProfile", menuName = "ScriptableObjects/Game/Pool/PooledSoundProfile", order = 2)]
    public class PooledSoundProfile : PooledBaseProfile
    {
        [Header("Audio")]
        [SerializeField]
        [Range(0, 1)]
        private float _volume = 1;

        [SerializeField]
        private bool _disableOnComplete;

        [SerializeField]
        private bool _allowPause;

        public float Volume => _volume;
        public bool DisableOnComplete => _disableOnComplete;
        public bool AllowPause => _allowPause;
    }
}