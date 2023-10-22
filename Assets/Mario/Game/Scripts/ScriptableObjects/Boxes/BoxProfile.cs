using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BoxProfile", menuName = "ScriptableObjects/Game/Boxes/BoxProfile", order = 0)]
    public class BoxProfile : ScriptableObject
    {
        [SerializeField] private PooledSoundProfile _hitSoundFXPoolReference;
        [SerializeField] private float _fallSpeed;
        [SerializeField] private float _maxFallSpeed;
        [SerializeField] private float _jumpAcceleration;

        public PooledSoundProfile HitSoundFXPoolReference => _hitSoundFXPoolReference;
        public float FallSpeed => _fallSpeed;
        public float MaxFallSpeed => _maxFallSpeed;
        public float JumpAcceleration => _jumpAcceleration;
    }
}