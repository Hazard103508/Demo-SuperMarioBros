using System;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace UnityShared.Behaviours.Various.RaycastRange
{
    public class RaycastRangeSquare : MonoBehaviour
    {
        [SerializeField] private Bounds<RaycastRange> _bounds;
    }
}