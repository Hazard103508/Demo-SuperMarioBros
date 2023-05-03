using UnityEditor;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace UnityShared.Editor.PropertyDrawers.Structs
{
    [CustomPropertyDrawer(typeof(MarginHorizontal))]
    public class MarginHorizontalPropertyDrawer : BasePropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawPrefixLabel(position, label);

            float labelLeftWidth = 30;
            float labelRightWidth = 40;
            float extraSpacing = 5;
            float fieldWidth = GetDynamicWidth(2, labelLeftWidth + labelRightWidth, extraSpacing);

            float xLabelL = FullRect.x;
            float xFieldL = xLabelL + labelLeftWidth;
            float xLabelR = xFieldL + fieldWidth + extraSpacing;
            float xFieldR = xLabelR + labelRightWidth;

            base.BeginPropertyDraw();

            base.DrawLabel(xLabelL, labelLeftWidth, "Left");
            base.DrawField(property, xFieldL, fieldWidth, "Left");
            base.DrawLabel(xLabelR, labelRightWidth, "Right");
            base.DrawField(property, xFieldR, fieldWidth, "Right");

            base.EndPropertyDraw();
        }
    }

}