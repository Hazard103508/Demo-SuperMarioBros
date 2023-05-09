using UnityEngine;
using UnityShared.Commons.Structs;

namespace UnityShared.Behaviours.Various.RaycastRange
{
    public class RaycastRangeRight : RaycastRange
    {
        protected override RayRange CalculateRayRange()
        {
            var b = new Bounds(transform.position, SpriteSize);
            return new RayRange(b.max.x, b.min.y + _profile.OffSet, b.max.x, b.max.y - _profile.OffSet, Vector2.right);
        }
    }
}