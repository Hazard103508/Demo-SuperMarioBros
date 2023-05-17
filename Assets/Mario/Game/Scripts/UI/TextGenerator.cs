using Mario.Game.ScriptableObjects.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Mario.Game.UI
{
    public class TextGenerator : MonoBehaviour
    {
        [SerializeField] private FontProfile _fontProfile;
        [SerializeField] private int _letterCount;
        [SerializeField] private string _text;
        [SerializeField] private float letterWidth;
        [SerializeField] private Image letterSpriteTemplate;

        private Image[] letters;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                UpdateText();
            }
        }

        private void Awake()
        {
            letters = new Image[_letterCount];
            for (int i = 0; i < _letterCount; i++)
            {
                var img = Instantiate(letterSpriteTemplate, letterSpriteTemplate.transform.parent);
                img.gameObject.SetActive(true);
                img.transform.localPosition += Vector3.right * letterWidth * i;
                letters[i] = img;
            }
        }
        private void OnEnable()
        {
            UpdateText();
        }
        private void UpdateText()
        {
            if (letters != null)
                for (int i = 0; i < _text.Length; i++)
                {
                    var img = letters[i];
                    img.sprite = _fontProfile.Sprites[_text[i]];
                }
        }
    }
}