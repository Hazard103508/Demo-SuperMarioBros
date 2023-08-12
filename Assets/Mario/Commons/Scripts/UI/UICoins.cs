using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UICoins : MonoBehaviour
    {
        #region Objects
        [SerializeField] private IconText label;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            Services.CoinService.CoinsChanged += OnCoinsChanged;
            OnCoinsChanged();
        }
        private void OnDestroy() => Services.CoinService.CoinsChanged -= OnCoinsChanged;
        #endregion

        #region Service Events	
        private void OnCoinsChanged() => label.Text = "x" + Services.CoinService.Coins.ToString("D2");
        #endregion
    }
}
