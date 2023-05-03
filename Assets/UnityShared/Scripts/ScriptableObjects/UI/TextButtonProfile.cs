using UnityEngine;
using UnityShared.Commons.Skins;

namespace UnityShared.ScriptableObjects.UI
{
    [CreateAssetMenu(fileName = "TextButtonProfile", menuName = "ScriptableObjects/UI/TextButton", order = 0)]
    public class TextButtonProfile : ScriptableObject
    {
        public RectTransformSkin RectTransform;
        public ImageSkin Image;
        public TextMeshProUGUISkin TextMeshPro;
    }
}