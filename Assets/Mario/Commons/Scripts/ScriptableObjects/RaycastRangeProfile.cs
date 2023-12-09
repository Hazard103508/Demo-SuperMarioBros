using System;
using UnityEngine;

namespace Mario.Commons.ScriptableObjects
{
    [CreateAssetMenu(fileName = "RaycastRangeProfile", menuName = "ScriptableObjects/GameObjects/RaycastRangeProfile", order = 1)]
    public class RaycastRangeProfile : ScriptableObject
    {
        [SerializeField] private LayerMask _blockLayers;
        [SerializeField] private LayerMask _otherLayers;
        [SerializeField] private RangeInfo _range;
        [SerializeField] private RayInfo _ray;

        public LayerMask BlockLayers => _blockLayers;
        public LayerMask OtherLayers => _otherLayers;
        public RangeInfo Range => _range;
        public RayInfo Ray => _ray;

        [Serializable]
        public class RangeInfo
        {
            [SerializeField] private Vector2 _startPoint;
            [SerializeField] private Vector2 _endPoint;
            [Range(1, 10)]
            [SerializeField] private int _count = 1;

            public Vector2 StartPoint => _startPoint;
            public Vector2 EndPoint => _endPoint;
            public int Count => _count;
        }
        [Serializable]
        public class RayInfo
        {
            [SerializeField] private Vector2 _direction;
            [SerializeField] private float _length;

            public Vector2 Direction => _direction;
            public float Length => _length;
        }
    }
}