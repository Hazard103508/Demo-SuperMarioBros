using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UITime : MonoBehaviour
    {
        [SerializeField] private IconText label;

        private void Awake()
        {
            Services.TimeService.OnTimeChanged.AddListener(OnTimeChanged);
            Services.TimeService.OnTimeStart.AddListener(OnTimeStart);

            label.gameObject.SetActive(false);
            OnTimeChanged();

            if (Services.GameDataService.CurrentMapProfile.Time.Type == Game.ScriptableObjects.Map.MapTimeType.None)
                Destroy(gameObject);
        }
        private void OnDestroy()
        {
            Services.TimeService.OnTimeChanged.RemoveListener(OnTimeChanged);
            Services.TimeService.OnTimeStart.RemoveListener(OnTimeStart);
        }

        private void OnTimeChanged() => label.Text = Services.TimeService.Time.ToString("D3");
        private void OnTimeStart() => label.gameObject.SetActive(true);
    }
}
