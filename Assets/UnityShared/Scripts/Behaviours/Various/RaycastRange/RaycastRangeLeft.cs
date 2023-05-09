using UnityEngine;
using UnityShared.Commons.Structs;

namespace UnityShared.Behaviours.Various.RaycastRange
{
    public class RaycastRangeLeft : RaycastRange
    {
        protected override RayRange CalculateRayRange()
        {
            var b = new Bounds(transform.position, SpriteSize);
            return new RayRange(b.min.x, b.min.y + _profile.OffSet, b.min.x, b.max.y - _profile.OffSet, Vector2.left);
        }
    }
}