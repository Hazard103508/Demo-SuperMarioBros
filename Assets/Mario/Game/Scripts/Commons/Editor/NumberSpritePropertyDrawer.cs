using UnityEditor;
using UnityEngine;
using UnityShared.Editor.PropertyDrawers;

namespace Mario.Game.Commons.Editor
{
    [CustomPropertyDrawer(typeof(NumberSprite))]
    public class NumberSpritePropertyDrawer : BasePropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawPrefixLabel(position, label);

            float fieldValueWidth = 30;
            float extraSpacing = 5;
            float fieldSpriteWidth = GetDynamicWidth(1, extraSpacing) - fieldValueWidth;

            float xValue = FullRect.x;
            float xSprite = xValue + fieldValueWidth + extraSpacing;

            base.BeginPropertyDraw();

            base.DrawField(property, xValue, fieldValueWidth, "Value");
            base.DrawField(property, xSprite, fieldSpriteWidth, "Sprite");
            base.EndPropertyDraw();
        }
    }
}