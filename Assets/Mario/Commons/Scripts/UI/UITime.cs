using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UITime : MonoBehaviour
    {
        #region Objects
        [SerializeField] private IconText label;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            Services.TimeService.TimeChangeded += OnTimeChanged;
            Services.TimeService.TimeStarted += OnTimeStart;

            label.gameObject.SetActive(false);
            OnTimeChanged();

            if (Services.GameDataService.CurrentMapProfile.Time.Type == Game.ScriptableObjects.Map.MapTimeType.None)
                Destroy(gameObject);
        }
        private void OnDestroy()
        {
            Services.TimeService.TimeChangeded -= OnTimeChanged;
            Services.TimeService.TimeStarted -= OnTimeStart;
        }
        #endregion

        #region Service Events
        private void OnTimeChanged() => label.Text = Services.TimeService.Time.ToString("D3");
        private void OnTimeStart() => label.gameObject.SetActive(true);
        #endregion
    }
}
