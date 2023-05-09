using System.Collections.Generic;
using UnityEngine;

namespace UnityShared.Commons.Structs
{
    public class RayHitInfo
    {
        public bool IsHiting { get; set; }
        public List<GameObject> hitObjects { get; set; }
    }
}