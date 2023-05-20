using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UIWorldName : MonoBehaviour
    {
        [SerializeField] private TextGenerator label;

        private void Awake()
        {
            label.Text = AllServices.GameDataService.MapProfile.WorldName;
        }
    }
}
