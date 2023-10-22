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
            label.Text = _levelService.MapProfile.WorldName;
        }
    }
}
