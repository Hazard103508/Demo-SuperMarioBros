using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.UI
{
    public class UIWorldName : MonoBehaviour
    {
        [SerializeField] private Mario.Game.UI.TextGenerator label;

        private void Awake()
        {
            label.Text = AllServices.GameDataService.MapProfile.WorldName;
        }
    }
}
