using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UICoins : MonoBehaviour
    {
        [SerializeField] private TextGenerator label;

        private void Awake()
        {
            OnCoinsChanged();
        }
        private void OnEnable() => AllServices.CoinService.OnCoinsChanged.AddListener(OnCoinsChanged);
        private void OnDisable() => AllServices.CoinService.OnCoinsChanged.RemoveListener(OnCoinsChanged);
        private void OnCoinsChanged() => label.Text = AllServices.CoinService.Coins.ToString("D2");
    }
}
