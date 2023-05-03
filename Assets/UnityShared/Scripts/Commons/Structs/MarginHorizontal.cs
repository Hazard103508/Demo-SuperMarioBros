using System;

namespace UnityShared.Commons.Structs
{
    [Serializable]
    public class MarginHorizontal
    {
        public MarginHorizontal()
        {
        }
        public MarginHorizontal(float left, float right)
        {
            this.Left = left;
            this.Right = left;
        }

        public float Left;
        public float Right;
    }
}