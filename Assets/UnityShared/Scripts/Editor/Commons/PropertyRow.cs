using UnityEditor;
using UnityEngine;
using UnityShared.Editor.Enums;

namespace UnityShared.Editor.Commons
{
    public class PropertyRow
    {
        public PropertyRow(SerializedProperty serializedProperty) : this(serializedProperty, null) { }
        public PropertyRow(SerializedProperty serializedProperty, GUIContent label)
        {
            this.Property = serializedProperty;
            this.Label = label;
        }

        public SerializedProperty Property { get; private set; }
        public GUIContent Label { get; private set; }
        public float Height { get => EditorGUI.GetPropertyHeight(Property); }
    }
}
