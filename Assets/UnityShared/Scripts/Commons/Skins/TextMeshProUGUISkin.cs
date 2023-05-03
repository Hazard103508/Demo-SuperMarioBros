using System;
using TMPro;
using UnityEngine;

namespace UnityShared.Commons.Skins
{
    [Serializable]
    public class TextMeshProUGUISkin
    {
        [field: SerializeField] public TMP_FontAsset FontAsset { get; set; }
        [field: SerializeField] public FontStyles FontStyle { get; set; }
        [field: SerializeField] public int FontSize { get; set; }
        [field: SerializeField] public bool AutoSize { get; set; }
        [field: SerializeField] public Color Color { get; set; }
    }
}