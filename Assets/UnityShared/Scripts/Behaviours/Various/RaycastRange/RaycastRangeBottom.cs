using UnityEngine;
using UnityShared.Commons.Structs;

namespace UnityShared.Behaviours.Various.RaycastRange
{
    public class RaycastRangeBottom : RaycastRange
    {
        protected override RayRange CalculateRayRange()
        {
            var b = new Bounds(transform.position, SpriteSize);
            return new RayRange(b.min.x + _profile.OffSet, b.min.y, b.max.x - _profile.OffSet, b.min.y, Vector2.down);
        }
    }
}