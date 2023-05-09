using System;
using UnityEngine;

namespace UnityShared.Commons.Structs
{
    [Serializable]
    public class Size2
    {
        public Size2()
        {
        }
        public Size2(float width, float height)
        {
            this.Width = width;
            this.Height = height;
        }

        public float Width;
        public float Height;

        public static implicit operator Vector3(Size2 size) => new Vector3(size.Width, size.Height);
    }
}