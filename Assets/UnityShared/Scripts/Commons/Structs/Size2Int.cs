using System;

namespace UnityShared.Commons.Structs
{
    [Serializable]
    public class Size2Int
    {
        public Size2Int()
        {
        }
        public Size2Int(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width;
        public int Height;
    }
}