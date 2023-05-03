using System;
using UnityEngine;

namespace UnityShared.Commons.PropertyAttributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class TagSelectorAttribute : PropertyAttribute
    {
        public bool UseDefaultTagFieldDrawer = false;
    }
}