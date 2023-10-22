using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Interactable
{
    [CreateAssetMenu(fileName = "FireballProfile", menuName = "ScriptableObjects/Game/Interactable/FireballProfile", order = 0)]
    public class FireballProfile : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _bounceSpeed;
        [SerializeField] private float _fallSpeed;
        [SerializeField] private float _maxFallSpeed;
        [SerializeField] private PooledObjectProfile _explotionPoolReference;
        [SerializeField] private PooledSoundProfile _hitSoundFXPoolReference;

        public float Speed => _speed;
        public float BounceSpeed => _bounceSpeed;
        public float FallSpeed => _fallSpeed;
        public float MaxFallSpeed => _maxFallSpeed;
        public PooledObjectProfile ExplotionPoolReference => _explotionPoolReference;
        public PooledSoundProfile HitSoundFXPoolReference => _hitSoundFXPoolReference;
    }
}