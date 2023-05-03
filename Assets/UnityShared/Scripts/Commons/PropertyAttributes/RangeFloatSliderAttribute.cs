using System;
using UnityEngine;

namespace UnityShared.Commons.PropertyAttributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class RangeFloatSliderAttribute : PropertyAttribute
    {
        public float Min;
        public float Max;

        public RangeFloatSliderAttribute(float min, float max)
        {
            this.Min = min;
            this.Max = max;
        }
    }
}

