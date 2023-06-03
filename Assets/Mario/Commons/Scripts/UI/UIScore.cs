using Mario.Application.Services;
using UnityEngine;

namespace Mario.Commons.UI
{
    public class UIScore : MonoBehaviour
    {
        [SerializeField] private IconText label;

        private void Awake()
        {
            AllServices.ScoreService.OnScoreChanged.AddListener(OnScoreChanged);
            OnScoreChanged();
        }
        private void OnDestroy() => AllServices.ScoreService.OnScoreChanged.RemoveListener(OnScoreChanged);
        private void OnScoreChanged() => label.Text = AllServices.ScoreService.Score.ToString("D6");
    }
}
