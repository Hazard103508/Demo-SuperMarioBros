using UnityEngine;

namespace Mario.Commons.Structs
{
    public struct RayRange
    {
        public RayRange(Vector2 start, Vector2 end, Vector2 dir) : this(start.x, start.y, end.x, end.y, dir)
        {
        }
        public RayRange(float x1, float y1, float x2, float y2, Vector2 dir)
        {
            Start = new Vector2(x1, y1);
            End = new Vector2(x2, y2);
            Dir = dir;
        }

        public readonly Vector2 Start, End, Dir;
    }
}