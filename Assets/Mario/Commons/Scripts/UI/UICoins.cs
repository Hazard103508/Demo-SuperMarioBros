using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UICoins : MonoBehaviour
    {
        [SerializeField] private IconText label;

        private void Awake()
        {
            Services.CoinService.OnCoinsChanged.AddListener(OnCoinsChanged);
            OnCoinsChanged();
        }
        private void OnDestroy() => Services.CoinService.OnCoinsChanged.RemoveListener(OnCoinsChanged);
        private void OnCoinsChanged() => label.Text = "x" + Services.CoinService.Coins.ToString("D2");
    }
}
