using UnityEditor;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace UnityShared.Editor.PropertyDrawers.Structs
{
    [CustomPropertyDrawer(typeof(RangeNumber<float>))]
    [CustomPropertyDrawer(typeof(RangeNumber<int>))]
    public class RangeNumberPropertyDrawer : BasePropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawPrefixLabel(position, label);

            float labelWidth = 30;
            float extraSpacing = 5;
            float fieldWidth = GetDynamicWidth(2, labelWidth * 2, extraSpacing);

            float xLabelMin = FullRect.x;
            float xFieldMin = xLabelMin + labelWidth;
            float xLabelMax = xFieldMin + fieldWidth + extraSpacing;
            float xFieldMax = xLabelMax + labelWidth;

            base.BeginPropertyDraw();
            
            base.DrawLabel(xLabelMin, labelWidth, "Min");
            base.DrawField(property, xFieldMin, fieldWidth, "Min");
            base.DrawLabel(xLabelMax, labelWidth, "Max");
            base.DrawField(property, xFieldMax, fieldWidth, "Max");

            base.EndPropertyDraw();
        }
    }
}
