using UnityEditor;
using UnityEngine;
using UnityShared.Commons.PropertyAttributes;

namespace UnityShared.Editor.PropertyDrawers.Attributes
{
    [CustomPropertyDrawer(typeof(RangeFloatSliderAttribute))]
    public class RangeFloatSliderPropertyDrawer : BasePropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawPrefixLabel(position, label);

            float spacing = 10;
            float fieldWidth = GetDynamicWidth(5, extraSpacing: spacing * 2);
            float slideWidth = fieldWidth * 3;

            float xFieldMin = FullRect.x;
            float xSlider = FullRect.x + fieldWidth + spacing;
            float xFieldMax = xSlider + slideWidth + spacing;

            var attr = base.GetAttribute<RangeFloatSliderAttribute>();

            base.BeginPropertyDraw();
            
            base.DrawField(property, xFieldMin, fieldWidth, "Min");
            base.DrawMinMaxSlider(property, xSlider, slideWidth, attr.Min, attr.Max, "Min", "Max");
            base.DrawField(property, xFieldMax, fieldWidth, "Max");

            base.EndPropertyDraw();
        }
    }
}
