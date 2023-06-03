using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UICoins : MonoBehaviour
    {
        [SerializeField] private IconText label;

        private void Awake()
        {
            OnCoinsChanged();
        }
        private void OnEnable() => AllServices.CoinService.OnCoinsChanged.AddListener(OnCoinsChanged);
        private void OnDisable() => AllServices.CoinService.OnCoinsChanged.RemoveListener(OnCoinsChanged);
        private void OnCoinsChanged() => label.Text = "x" + AllServices.CoinService.Coins.ToString("D2");
    }
}
