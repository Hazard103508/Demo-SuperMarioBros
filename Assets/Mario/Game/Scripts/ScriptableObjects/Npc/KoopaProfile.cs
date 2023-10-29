using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "KoopaProfile", menuName = "ScriptableObjects/Game/Npc/KoopaProfile", order = 1)]
    public class KoopaProfile : ScriptableObject
    {
        [SerializeField] private PooledSoundProfile _hitSoundFXPoolReference;
        [SerializeField] private PooledSoundProfile _kickSoundFXPoolReference;
        [SerializeField] private PooledSoundProfile _bouncingSoundFXPoolReference;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _bouncingSpeed;
        [SerializeField] private float _fallSpeed;
        [SerializeField] private float _maxFallSpeed;
        [SerializeField] private float _jumpAcceleration;
        [SerializeField] private int _pointsHit1;
        [SerializeField] private int _pointsHit2;
        [SerializeField] private int _pointsKill;

        public PooledSoundProfile HitSoundFXPoolReference => _hitSoundFXPoolReference;
        public PooledSoundProfile KickSoundFXPoolReference => _kickSoundFXPoolReference;
        public PooledSoundProfile BouncingSoundFXPoolReference => _bouncingSoundFXPoolReference;
        public float MoveSpeed => _moveSpeed;
        public float BouncingSpeed => _bouncingSpeed;
        public float FallSpeed => _fallSpeed;
        public float MaxFallSpeed => _maxFallSpeed;
        public float JumpAcceleration => _jumpAcceleration;
        public int PointsHit1 => _pointsHit1;
        public int PointsHit2 => _pointsHit2;
        public int PointsKill => _pointsKill;

    }
}