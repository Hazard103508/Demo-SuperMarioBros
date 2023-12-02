using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UIWorldName : MonoBehaviour
    {
        private ILevelService _levelService;
        [SerializeField] private IconText label;

        private void Awake()
        {
            _levelService = ServiceLocator.Current.Get<ILevelService>();
            _levelService.StartLoading += OnStartLoading;

            SetWorldName();
        }
        private void OnDestroy()
        {
            _levelService.StartLoading -= OnStartLoading;
        }

        private void OnStartLoading(LevelService.StartLoadingEvent obj) => SetWorldName();
        private void SetWorldName() => label.Text = _levelService.MapProfile.WorldName;
    }
}
