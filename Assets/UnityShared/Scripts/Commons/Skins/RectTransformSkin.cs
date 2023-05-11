using System;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace UnityShared.Commons.Skins
{
    [Serializable]
    public class RectTransformSkin
    {
        public RectTransformSkin()
        {
            Scale = Vector3.one;
            Size = new Size2(100, 100);
        }

        public Size2 Size;
        public float Rotation;
        public Vector2 Scale;
    }
}