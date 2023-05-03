using TMPro;
using UnityShared.Commons.Skins;

namespace UnityShared.Extensions.Skins
{
    public static class TextMeshProUGUIExtension
    {
        public static void SetSkin(this TextMeshProUGUI label, TextMeshProUGUISkin skin)
        {
            label.font = skin.FontAsset;
            label.fontStyle = skin.FontStyle;
            label.fontSize = skin.FontSize;
            label.autoSizeTextContainer = skin.AutoSize;
            label.color = skin.Color;
        }
    }
}