using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UICoins : MonoBehaviour
    {
        #region Objects
        private ICoinService _coinService;

        [SerializeField] private IconText label;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _coinService = ServiceLocator.Current.Get<ICoinService>();
            _coinService.CoinsChanged += OnCoinsChanged;
            OnCoinsChanged();
        }
        private void OnDestroy() => _coinService.CoinsChanged -= OnCoinsChanged;
        #endregion

        #region Service Events	
        private void OnCoinsChanged() => label.Text = "x" + _coinService.Coins.ToString("D2");
        #endregion
    }
}
