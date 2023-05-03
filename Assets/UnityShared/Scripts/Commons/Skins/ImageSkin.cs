using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityShared.Commons.Skins
{
    [Serializable]
    public class ImageSkin
    {
        public ImageSkin()
        {
            Color = Color.white;
            Type = Image.Type.Simple;
        }

        public Sprite Sprite;
        public Color Color;
        public Image.Type Type;
    }
}