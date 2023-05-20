using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UIPlayer : MonoBehaviour
    {
        [SerializeField] private TextGenerator label;

        private void Awake()
        {
            label.Text = AllServices.GameDataService.PlayerProfile.PlayerName;
        }
    }
}
