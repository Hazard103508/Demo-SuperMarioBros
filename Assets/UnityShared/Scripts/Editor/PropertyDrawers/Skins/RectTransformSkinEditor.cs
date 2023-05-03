using UnityEditor;
using UnityEngine;
using UnityShared.Commons.Skins;
using UnityShared.Commons.Structs;
using UnityShared.Editor.Commons;
using UnityShared.Enums;

namespace UnityShared.Editor.PropertyDrawers.Skins
{
    /*[CustomPropertyDrawer(typeof(RectTransformSkin))]
    public class RectTransformSkinEditor : BasePropertyDrawer
    {
        public bool unfold = false;
        float folderHeight = 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            folderHeight = base.GetDefaultPropertyHeight();

            base.FullRect = position;
            base.BeginPropertyDraw();
            var state = base.DrawFolder(property, label);

            if (state == Enums.FolderState.EXPANDED)
            {
                var folderPos = position.position + new Vector2(0, base.GetDefaultPropertyHeight());
            
                var propAnchorH = property.FindPropertyRelative("AnchorHorizontal");
                var propAnchorV = property.FindPropertyRelative("AnchorVertical");
                var propMarginH = property.FindPropertyRelative("MarginHorizontal");
                var propMarginV = property.FindPropertyRelative("MarginVertical");
                var propSize = property.FindPropertyRelative("Size");
                var propRotation = property.FindPropertyRelative("Rotation");
                var propScale = property.FindPropertyRelative("Scale");

                PropertyGroup propertyGroup = new PropertyGroup();
                propertyGroup.Add(propAnchorH);
                propertyGroup.Add(propAnchorV);
                propertyGroup.Add(propMarginH);
                propertyGroup.Add(propMarginV);
                propertyGroup.Add(propSize);
                propertyGroup.Add(propRotation);
                propertyGroup.Add(propScale);
            
                base.DrawVerticalGroup(propertyGroup, folderPos, position.width, 1);
                folderHeight = base.GetDefaultPropertyHeight() + propertyGroup.Height;
            }
            base.EndPropertyDraw();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return folderHeight;
        }
    }
    */

}