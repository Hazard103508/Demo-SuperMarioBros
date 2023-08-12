using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UIScore : MonoBehaviour
    {
        #region Objects
        [SerializeField] private IconText label;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            Services.ScoreService.ScoreChanged += OnScoreChanged;
            OnScoreChanged();
        }
        private void OnDestroy() => Services.ScoreService.ScoreChanged -= OnScoreChanged;
        #endregion

        #region Service Events	
        private void OnScoreChanged() => label.Text = Services.ScoreService.Score.ToString("D6");
        #endregion
    }
}
