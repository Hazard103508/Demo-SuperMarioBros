using System.Drawing.Printing;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;
using UnityShared.Commons.Structs;
using UnityShared.Editor.Commons;
using UnityShared.Editor.Enums;

namespace UnityShared.Editor.PropertyDrawers
{
    public class BasePropertyDrawer : PropertyDrawer
    {
        protected Rect FullRect { get; set; }
        protected float Width => FullRect.width;
        protected float Height => FullRect.height;

        protected void BeginPropertyDraw()
        {
            EditorGUI.indentLevel = 0;
            EditorGUI.BeginChangeCheck();
        }
        protected void EndPropertyDraw()
        {
            EditorGUI.EndChangeCheck();
        }

        protected void DrawPrefixLabel(Rect position, GUIContent label) => FullRect = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        protected void DrawDefault(SerializedProperty property, GUIContent Label, float x, float y)
        {
            var rect = new Rect(x, y, Width, 18);
            EditorGUI.PropertyField(rect, property, Label);
        }
        protected void DrawLabel(float x, float width, string label)
        {
            var rect = new Rect(x, FullRect.y, width, Height);
            EditorGUI.LabelField(rect, label);
        }
        protected void DrawField(SerializedProperty property, string propertyName) => DrawField(property, FullRect.x, Width, propertyName);
        protected void DrawField(SerializedProperty property, float x, float width, string propertyName)
        {
            var rect = new Rect(x, FullRect.y, width, Height);
            EditorGUI.PropertyField(rect, property.FindPropertyRelative(propertyName), GUIContent.none);
        }
        protected void DrawMinMaxSlider(SerializedProperty property, float x, float width, float minLimit, float maxLimit, string propertyNameMin, string propertyNameMax)
        {
            var min = property.FindPropertyRelative(propertyNameMin);
            var max = property.FindPropertyRelative(propertyNameMax);

            float minVal = min.floatValue;
            float maxVal = max.floatValue;

            var rect = new Rect(x, FullRect.y, width, Height);
            EditorGUI.MinMaxSlider(rect, ref minVal, ref maxVal, minLimit, maxLimit);

            minVal = Mathf.Min(Mathf.Max(minVal, minLimit), maxVal);
            maxVal = Mathf.Max(Mathf.Min(maxVal, maxLimit), minVal);

            min.floatValue = minVal;
            max.floatValue = maxVal;
        }
        protected void DrawPopup(SerializedProperty property, PopupItems popupItems) => DrawPopup(property, FullRect.x, Width, popupItems);
        protected void DrawPopup(SerializedProperty property, float x, float width, PopupItems popupItems)
        {
            var rect = new Rect(x, FullRect.y, width, Height);
            
            int index = popupItems[property.stringValue];
            index = EditorGUI.Popup(rect, index, popupItems);
            property.stringValue = popupItems[index];
        }
        protected void DrawTagField(SerializedProperty property) => DrawTagField(property, FullRect.x, Width);
        protected void DrawTagField(SerializedProperty property, float x, float width)
        {
            var rect = new Rect(x, FullRect.y, width, Height);
            property.stringValue = EditorGUI.TagField(rect, property.stringValue);
        }
        protected void DrawVerticalGroup(PropertyGroup group, Vector2 position, float width, int indentLevel)
        {
            float y = 0;
            foreach (PropertyRow p in group.Properties)
            {
                EditorGUI.indentLevel = indentLevel;

                var pos = position + new Vector2(0, y);
                DrawDefault(p.Property, p.Label, pos.x, pos.y);

                y += EditorGUI.GetPropertyHeight(p.Property) + group.RowMargin;
            }
        }
        public FolderState DrawFolder(SerializedProperty property, GUIContent label)
        {
            var rect = new Rect(FullRect.x, FullRect.y, FullRect.width, GetDefaultPropertyHeight());
            property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label);
            return property.isExpanded ? FolderState.EXPANDED : FolderState.COLLAPSED;
        }

        protected float GetDynamicWidth(int controlsCount, float labelWidth = 0, float extraSpacing = 0) => ((this.Width - labelWidth - extraSpacing) / controlsCount);
        protected T GetAttribute<T>() where T : PropertyAttribute => (T)attribute;
        protected float GetDefaultPropertyHeight() => base.GetPropertyHeight(null, null);
    }
}
