
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityShared.Enums;
using UnityShared.Extensions.Unity3D;

namespace UnityShared.Behaviours.UI
{
    public class Switch : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private SwitchImages images;
        [SerializeField] private SwitchLabels labels;
        [SerializeField] private bool isOn;

        public UnityEvent onSwitchChanged;

        public bool IsOn
        {
            get => isOn;
            set
            {
                if (isOn != value)
                {
                    isOn = value;
                    UpdateToggle();
                    onSwitchChanged.Invoke();
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            this.IsOn = !this.IsOn;
        }

        private void OnValidate()
        {
            UpdateToggle();
        }

        private void UpdateToggle()
        {
            var recTransform = (RectTransform)images.toggle.transform;

            images.backgroundOn.gameObject.SetActive(this.IsOn);
            images.backgroundOff.gameObject.SetActive(!this.IsOn);
            labels.labelOn.gameObject.SetActive(this.IsOn);
            labels.labelOff.gameObject.SetActive(!this.IsOn);

            float margin = -10;
            float widgth = 86;
            if (this.IsOn)
            {
                recTransform.SetAnchor(RectTransformAnchorHorizontal.RIGHT, RectTransformAnchorVertical.MIDDLE);
                recTransform.SetRightMargin(margin);
                recTransform.SetWidth(widgth);
            }
            else
            {
                recTransform.SetAnchor(RectTransformAnchorHorizontal.LEFT, RectTransformAnchorVertical.MIDDLE);
                recTransform.SetLeftMargin(margin);
                recTransform.SetWidth(widgth);
            }
        }

        [Serializable]
        private class SwitchImages
        {
            public Image toggle = null;
            public Image backgroundOn = null;
            public Image backgroundOff = null;
        }
        [Serializable]
        private class SwitchLabels
        {
            public TextMeshProUGUI labelOff = null;
            public TextMeshProUGUI labelOn = null;
        }
    }
}