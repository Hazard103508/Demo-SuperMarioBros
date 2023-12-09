using System.Collections.Generic;
using UnityEngine;

namespace Mario.Commons.Structs
{
    public class RayHitInfo
    {
        public bool IsBlock { get; set; }
        public List<HitObject> hitObjects { get; set; }
    }
    public class HitObject
    {
        public bool IsBlock { get; set; }
        public GameObject Object { get; set; }
        public Vector2 Point { get; set; }
        public Vector2 RelativePosition { get; set; }
    }
}