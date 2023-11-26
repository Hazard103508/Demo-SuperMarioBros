using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "StarProfile", menuName = "ScriptableObjects/Game/Items/StarProfile", order = 3)]
    public class StarProfile : ScriptableObject
    {
        [SerializeField] private int _points;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _bounceSpeed;
        [SerializeField] private float _fallSpeed;
        [SerializeField] private float _maxFallSpeed;

        public int Points => _points;
        public float MoveSpeed => _moveSpeed;
        public float BounceSpeed => _bounceSpeed;
        public float FallSpeed => _fallSpeed;
        public float MaxFallSpeed => _maxFallSpeed;
    }
}