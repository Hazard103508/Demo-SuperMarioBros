using UnityEngine;
using UnityShared.Commons.Structs;

namespace UnityShared.Behaviours.Various.RaycastRange
{
    public class RaycastRangeTop : RaycastRange
    {
        protected override RayRange CalculateRayRange()
        {
            var b = new Bounds(transform.position, SpriteSize);
            return new RayRange(b.min.x + _profile.OffSet, b.max.y, b.max.x - _profile.OffSet, b.max.y, Vector2.up);
        }
    }
}