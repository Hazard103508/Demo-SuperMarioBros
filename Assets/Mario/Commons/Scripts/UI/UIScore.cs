using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UIScore : MonoBehaviour
    {
        [SerializeField] private IconText label;

        private void Awake()
        {
            Services.ScoreService.OnScoreChanged.AddListener(OnScoreChanged);
            OnScoreChanged();
        }
        private void OnDestroy() => Services.ScoreService.OnScoreChanged.RemoveListener(OnScoreChanged);
        private void OnScoreChanged() => label.Text = Services.ScoreService.Score.ToString("D6");
    }
}
