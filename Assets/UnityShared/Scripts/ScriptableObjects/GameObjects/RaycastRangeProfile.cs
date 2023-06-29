using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityShared.Commons.Structs;

namespace UnityShared.ScriptableObjects.GameObjects
{
    [CreateAssetMenu(fileName = "RaycastRangeProfile", menuName = "ScriptableObjects/GameObjects/RaycastRangeProfile", order = 1)]
    public class RaycastRangeProfile : ScriptableObject
    {
        public LayerMask BlockLayers;
        public LayerMask OtherLayers;

        public float OffSet;

        public RangeInfo Range;
        public RayInfo Ray;

        [Serializable]
        public class RangeInfo
        {
            public Vector2 StartPoint;
            public Vector2 EndPoint;
            [Range(1, 10)] public int Count;
        }
        [Serializable]
        public class RayInfo
        {
            public Vector2 Direction;
            public float Length;
        }
    }
}