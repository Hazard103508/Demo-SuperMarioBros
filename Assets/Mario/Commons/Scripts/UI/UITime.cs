using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UITime : MonoBehaviour
    {
        [SerializeField] private IconText label;

        private void Awake()
        {
            AllServices.TimeService.OnTimeChanged.AddListener(OnTimeChanged);
            AllServices.TimeService.OnTimeStart.AddListener(OnTimeStart);
            AllServices.TimeService.ResetTimer();

            label.gameObject.SetActive(false);
            OnTimeChanged();

            if (AllServices.GameDataService.CurrentMapProfile.Time.Type == Game.ScriptableObjects.Map.MapTimeType.None)
                Destroy(gameObject);
        }
        private void OnDestroy()
        {
            AllServices.TimeService.OnTimeChanged.RemoveListener(OnTimeChanged);
            AllServices.TimeService.OnTimeStart.RemoveListener(OnTimeStart);
        }

        private void OnTimeChanged() => label.Text = AllServices.TimeService.Time.ToString("D3");
        private void OnTimeStart() => label.gameObject.SetActive(true);
    }
}
