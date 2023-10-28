using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "MushroomProfile", menuName = "ScriptableObjects/Game/Items/MushroomProfile", order = 2)]
    public class MushroomProfile : ScriptableObject
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _fallSpeed;
        [SerializeField] private float _maxFallSpeed;
        [SerializeField] private float _jumpAcceleration;

        public float MoveSpeed => _moveSpeed;
        public float FallSpeed => _fallSpeed;
        public float MaxFallSpeed => _maxFallSpeed;
        public float JumpAcceleration => _jumpAcceleration;
    }
}