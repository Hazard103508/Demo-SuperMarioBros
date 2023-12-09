using System;

namespace Mario.Commons.Structs
{
    [Serializable]
    public struct RangeNumber<T> where T : IComparable<T>
    {
        public RangeNumber(T min, T max)
        {
            this.Min = min;
            this.Max = max;
        }

        public T Min;
        public T Max;
    }
}