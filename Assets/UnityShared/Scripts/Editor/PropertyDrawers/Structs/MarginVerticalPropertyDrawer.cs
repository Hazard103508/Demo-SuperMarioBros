using UnityEditor;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace UnityShared.Editor.PropertyDrawers.Structs
{
    [CustomPropertyDrawer(typeof(MarginVertical))]
    public class MarginVerticalPropertyDrawer : BasePropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawPrefixLabel(position, label);
            
            float labelTopWidth = 30;
            float labelBottomWidth = 50;
            float extraSpacing = 5;
            float fieldWidth = GetDynamicWidth(2, labelTopWidth + labelBottomWidth, extraSpacing);

            float xLabelT= FullRect.x;
            float xFieldT = xLabelT + labelTopWidth;
            float xLabelB = xFieldT + fieldWidth + extraSpacing;
            float xFieldB = xLabelB + labelBottomWidth;
            
            base.BeginPropertyDraw();
            
            base.DrawLabel(xLabelT, labelTopWidth, "Top");
            base.DrawField(property, xFieldT, fieldWidth, "Top");
            base.DrawLabel(xLabelB, labelBottomWidth, "Bottom");
            base.DrawField(property, xFieldB, fieldWidth, "Bottom");
            
            base.EndPropertyDraw();
        }
    }

}