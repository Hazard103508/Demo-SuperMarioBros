using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UITime : MonoBehaviour
    {
        [SerializeField] private TextGenerator label;

        private void Awake()
        {
            OnTimeChanged();
        }
        private void OnEnable() => AllServices.TimeService.OnTimeChanged.AddListener(OnTimeChanged);
        private void OnDisable() => AllServices.TimeService.OnTimeChanged.RemoveListener(OnTimeChanged);
        private void OnTimeChanged() => label.Text = AllServices.TimeService.Time.ToString("D3");
    }
}