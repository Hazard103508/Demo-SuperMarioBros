using System.Collections.Generic;
using UnityEngine;

namespace UnityShared.Commons.Structs
{
    public class RayHitInfo
    {
        public bool IsBlock { get; set; }
        public List<GameObject> hitObjects { get; set; }
    }
}