using System.Text;
using TMPro;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class IconText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private string _text;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                WriteText();
            }
        }

        private void OnValidate() => WriteText();

        private void WriteText()
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in _text)
                sb.Append($"<sprite name=\"{c}\">");

            label.text = sb.ToString();
        }
    }
}