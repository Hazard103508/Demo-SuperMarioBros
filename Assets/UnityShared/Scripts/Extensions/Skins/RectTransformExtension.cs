using UnityEngine;
using UnityShared.Commons.Skins;

namespace UnityShared.Extensions.Skins
{
    public static class RectTransformExtension
    {
        public static void SetSkin(this RectTransform rt, RectTransformSkin skin)
        {
            rt.rotation = Quaternion.Euler(Vector3.forward * skin.Rotation);
            rt.localScale = skin.Scale;
            rt.sizeDelta = new Vector2(skin.Size.Width, skin.Size.Height);
        }
    }
}