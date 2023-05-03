using System;

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
    }
}