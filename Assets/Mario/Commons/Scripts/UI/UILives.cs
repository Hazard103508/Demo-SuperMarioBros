using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UILives : MonoBehaviour
    {
        #region Objects
        [SerializeField] private IconText label;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            Services.PlayerService.LivesAdded += OnLivesChanged;
            OnLivesChanged();
        }
        private void OnDestroy() => Services.PlayerService.LivesAdded -= OnLivesChanged;
        #endregion

        #region Service Events		
        private void OnLivesChanged() => label.Text = Services.PlayerService.Lives.ToString();
        #endregion

    }
}
