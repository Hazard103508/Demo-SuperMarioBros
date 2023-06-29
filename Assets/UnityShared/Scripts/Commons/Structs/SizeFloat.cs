using System;
using UnityEngine;

namespace UnityShared.Commons.Structs
{
    [Serializable]
    public class SizeFloat
    {
        public static SizeFloat zero = new SizeFloat();

        public SizeFloat()
        {
        }
        public SizeFloat(float width, float height)
        {
            this.Width = width;
            this.Height = height;
        }

        public float Width;
        public float Height;

        public static implicit operator Vector3(SizeFloat size) => new Vector3(size.Width, size.Height);
    }
}