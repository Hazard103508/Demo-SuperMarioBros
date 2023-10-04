using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UIScore : MonoBehaviour
    {
        #region Objects
        private IScoreService _scoreService;

        [SerializeField] private IconText label;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();

            _scoreService.ScoreChanged += OnScoreChanged;
            OnScoreChanged();
        }
        private void OnDestroy() => _scoreService.ScoreChanged -= OnScoreChanged;
        #endregion

        #region Service Events	
        private void OnScoreChanged() => label.Text = _scoreService.Score.ToString("D6");
        #endregion
    }
}
