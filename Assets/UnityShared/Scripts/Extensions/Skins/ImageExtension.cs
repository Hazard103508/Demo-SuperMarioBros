using UnityEngine.UI;
using UnityShared.Commons.Skins;

namespace UnityShared.Extensions.Skins
{
    public static class ImageExtension
    {
        public static void SetSkin(this Image img, ImageSkin skin)
        {
            img.color = skin.Color;
            img.sprite = skin.Sprite;
            img.type = skin.Type;
        }
    }
}