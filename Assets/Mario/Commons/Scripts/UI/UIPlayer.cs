using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UIPlayer : MonoBehaviour
    {
        [SerializeField] private IconText label;

        private void Awake()
        {
            label.Text = Services.GameDataService.PlayerProfile.PlayerName;
        }
    }
}
