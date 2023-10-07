using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UITime : MonoBehaviour
    {
        #region Objects
        private ITimeService _timeService;
        private ILevelService _levelService;

        [SerializeField] private IconText label;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _timeService = ServiceLocator.Current.Get<ITimeService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();

            _timeService.TimeChangeded += OnTimeChanged;
            _timeService.TimeStarted += OnTimeStart;

            label.gameObject.SetActive(false);
            OnTimeChanged();

            if (_levelService.CurrentMapProfile.Time.Type == Game.ScriptableObjects.Map.MapTimeType.None)
                Destroy(gameObject);
        }
        private void OnDestroy()
        {
            _timeService.TimeChangeded -= OnTimeChanged;
            _timeService.TimeStarted -= OnTimeStart;
        }
        #endregion

        #region Service Events
        private void OnTimeChanged() => label.Text = _timeService.Time.ToString("D3");
        private void OnTimeStart() => label.gameObject.SetActive(true);
        #endregion
    }
}
