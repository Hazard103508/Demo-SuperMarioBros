using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UnityShared.Editor.Commons
{
    public class PropertyGroup
    {
        public PropertyGroup()
        {
            Properties = new List<PropertyRow>();
        }
        public List<PropertyRow> Properties { get; private set; }
        public float Height { get => Properties.Sum(p => p.Height) + 10; }
        public float RowMargin { get; set; } = 2;

        public void Add(SerializedProperty serializedProperty, string label = "")
        {
            GUIContent guiLabel = string.IsNullOrWhiteSpace(label) ? null : new GUIContent(label);
            this.Properties.Add(new PropertyRow(serializedProperty, guiLabel));
        }
    }
}
