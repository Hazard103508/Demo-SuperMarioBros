using UnityEngine;
using UnityShared.Commons.PropertyAttributes;

namespace UnityShared.ScriptableObjects.GameObjects
{
    [CreateAssetMenu(fileName = "RaycastRangeProfile", menuName = "ScriptableObjects/GameObjects/RaycastRangeProfile", order = 1)]
    public class RaycastRangeProfile : ScriptableObject
    {
        public LayerMask BlockLayers;
        public LayerMask OtherLayers;

        public float OffSet;
        [Range(1, 10)] public int RayCount;
        public float DetectionRayLength;
    }
}