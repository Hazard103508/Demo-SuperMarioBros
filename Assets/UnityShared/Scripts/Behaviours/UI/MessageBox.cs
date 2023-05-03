using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityShared.Behaviours.Handlers;
using UnityShared.ScriptableObjects.UI;

namespace UnityShared.Behaviours.UI
{
    [RequireComponent(typeof(PanelHandler))]
    public class MessageBox : MonoBehaviour
    {
        private TMPro.TextMeshProUGUI message;
        private Image icon;
        public MessageBoxProfile data;

        private void Awake()
        {
            var content = transform.Find("content");
            message = content.Find("message").GetComponent<TextMeshProUGUI>();
            icon = content.Find("icon").GetComponent<Image>();
        }
        public void OnEnable()
        {
            message.text = data.message;
            icon.sprite = data.icon;
        }
    }
}