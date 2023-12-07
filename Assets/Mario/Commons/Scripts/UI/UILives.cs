using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UILives : MonoBehaviour
    {
        #region Objects
        private IPlayerService _playerService;

        [SerializeField] private IconText label;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _playerService = ServiceLocator.Current.Get<IPlayerService>();

            _playerService.LivesAdded += OnLivesChanged;
            OnLivesChanged();
        }
        private void OnDestroy() => _playerService.LivesAdded -= OnLivesChanged;
        private void OnEnable() => OnLivesChanged();
        #endregion

        #region Service Events		
        private void OnLivesChanged() => label.Text = _playerService.Lives.ToString();
        #endregion

    }
}
