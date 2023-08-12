using System.Text;
using TMPro;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class IconText : MonoBehaviour
    {
        #region Objects
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private string _text;
        #endregion

        #region Properties
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                WriteText();
            }
        }
        #endregion

        #region Unity Methods
        private void OnValidate() => WriteText();
        #endregion

        #region Private Methods
        private void WriteText()
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in _text)
                sb.Append($"<sprite name=\"{c}\">");

            label.text = sb.ToString();
        }
        #endregion
    }
}