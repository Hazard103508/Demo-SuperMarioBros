using System;

namespace UnityShared.Commons.Structs
{
    [Serializable]
    public class MarginVertical
    {
        public MarginVertical()
        {
        }
        public MarginVertical(float top, float bottom)
        {
            this.Top = top;
            this.Bottom = bottom;
        }

        public float Top;
        public float Bottom;
    }
}