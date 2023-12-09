using System;

namespace Mario.Commons.Structs
{
    [Serializable]
    public class Bounds<T>
    {
        public Bounds()
        {
        }

        public T left;
        public T right;
        public T top;
        public T bottom;
    }
}