using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "GoombaProfile", menuName = "ScriptableObjects/Game/Npc/GoombaProfile", order = 0)]
    public class GoombaProfile : ScriptableObject
    {
        [SerializeField]private float _moveSpeed;
        [SerializeField]private float _fallSpeed;
        [SerializeField]private float _maxFallSpeed;
        [SerializeField]private float _jumpAcceleration;
        [SerializeField] private int _points;

        public float MoveSpeed => _moveSpeed;
        public float FallSpeed => _fallSpeed;
        public float MaxFallSpeed => _maxFallSpeed;
        public float JumpAcceleration => _jumpAcceleration;
        public int Points => _points;
    }
}