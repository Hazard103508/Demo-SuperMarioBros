using UnityEngine;
using UnityShared.Enums;

namespace UnityShared.ScriptableObjects.GameObjects
{
    [CreateAssetMenu(fileName = "RaycastRangeProfile", menuName = "ScriptableObjects/GameObjects/RaycastRangeProfile", order = 1)]
    public class RaycastRangeProfile : ScriptableObject
    {
        public LayerMask GroundLayer;
        public float OffSet;
        public int RayCount;
        public float DetectionRayLength;
    }
}