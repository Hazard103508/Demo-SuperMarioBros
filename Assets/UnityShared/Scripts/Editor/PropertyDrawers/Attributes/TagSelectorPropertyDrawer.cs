using UnityEditor;
using UnityEngine;
using UnityShared.Commons.PropertyAttributes;
using UnityShared.Editor.Commons;

namespace UnityShared.Editor.PropertyDrawers.Attributes
{
    [CustomPropertyDrawer(typeof(TagSelectorAttribute))]
    public class TagSelectorPropertyDrawer : BasePropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawPrefixLabel(position, label);

            if (property.propertyType != SerializedPropertyType.String)
                return;

            var attr = base.GetAttribute<TagSelectorAttribute>();

            base.BeginPropertyDraw();
            if (attr.UseDefaultTagFieldDrawer)
                base.DrawTagField(property);
            else
            {
                PopupItems popupItems = new PopupItems();
                popupItems.Add("<NoTag>");
                popupItems.Add(UnityEditorInternal.InternalEditorUtility.tags);

                base.DrawPopup(property, popupItems);
            }
            base.EndPropertyDraw();
        }
    }
}