using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "GoombaProfile", menuName = "ScriptableObjects/Game/Npc/GoombaProfile", order = 0)]
    public class GoombaProfile : ScriptableObject
    {
        [SerializeField] private PooledSoundProfile _hitSoundFXPoolReference;
        [SerializeField] private PooledSoundProfile _kickSoundFXPoolReference;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _fallSpeed;
        [SerializeField] private float _maxFallSpeed;
        [SerializeField] private float _jumpAcceleration;
        [SerializeField] private int _points;

        public PooledSoundProfile HitSoundFXPoolReference => _hitSoundFXPoolReference;
        public PooledSoundProfile KickSoundFXPoolReference => _kickSoundFXPoolReference;
        public float MoveSpeed => _moveSpeed;
        public float FallSpeed => _fallSpeed;
        public float MaxFallSpeed => _maxFallSpeed;
        public float JumpAcceleration => _jumpAcceleration;
        public int Points => _points;
    }
}