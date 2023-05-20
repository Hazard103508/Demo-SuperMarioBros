using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.UI
{
    public class UIPlayer : MonoBehaviour
    {
        [SerializeField] private Mario.Game.UI.TextGenerator label;

        private void Awake()
        {
            label.Text = AllServices.GameDataService.PlayerProfile.PlayerName;
        }
    }
}
