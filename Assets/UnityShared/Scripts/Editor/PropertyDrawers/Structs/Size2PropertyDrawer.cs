using UnityEditor;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace UnityShared.Editor.PropertyDrawers.Structs
{
    [CustomPropertyDrawer(typeof(SizeFloat))]
    [CustomPropertyDrawer(typeof(Size2Int))]
    public class Size2PropertyDrawer : BasePropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawPrefixLabel(position, label);

            float labelWidth = 40;
            float extraSpacing = 5;
            float fieldWidth = GetDynamicWidth(2, labelWidth * 2, extraSpacing);

            float xLabelW = FullRect.x;
            float xFieldW = xLabelW + labelWidth;
            float xLabelH = xFieldW + fieldWidth + extraSpacing;
            float xFieldH = xLabelH + labelWidth;

            base.BeginPropertyDraw();

            base.DrawLabel(xLabelW, labelWidth, "Width");
            base.DrawField(property, xFieldW, fieldWidth, "Width");
            base.DrawLabel(xLabelH, labelWidth, "Height");
            base.DrawField(property, xFieldH, fieldWidth, "Height");

            base.EndPropertyDraw();
        }
    }

}