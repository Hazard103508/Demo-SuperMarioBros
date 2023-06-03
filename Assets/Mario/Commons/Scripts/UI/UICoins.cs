using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UICoins : MonoBehaviour
    {
        [SerializeField] private IconText label;

        private void Awake()
        {
            AllServices.CoinService.OnCoinsChanged.AddListener(OnCoinsChanged);
            OnCoinsChanged();
        }
        private void OnDestroy() => AllServices.CoinService.OnCoinsChanged.RemoveListener(OnCoinsChanged);
        private void OnCoinsChanged() => label.Text = "x" + AllServices.CoinService.Coins.ToString("D2");
    }
}
