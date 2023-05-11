using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityShared.Extensions.Skins;
using UnityShared.ScriptableObjects.UI;

public class TextButton : MonoBehaviour
{
    [SerializeField] private TextButtonProfile _profile;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnValidate()
    {
        if (_profile == null)
            return;

        _rectTransform.SetSkin(_profile.RectTransform);
        _image.SetSkin(_profile.Image);

    }
}
