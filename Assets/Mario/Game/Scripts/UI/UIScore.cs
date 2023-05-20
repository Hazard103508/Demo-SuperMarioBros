using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.UI
{
    public class UIScore : MonoBehaviour
    {
        [SerializeField] private Mario.Game.UI.TextGenerator label;

        private void Awake()
        {
            OnScoreChanged();
        }
        private void OnEnable() => AllServices.ScoreService.OnScoreChanged.AddListener(OnScoreChanged);
        private void OnDisable() => AllServices.ScoreService.OnScoreChanged.RemoveListener(OnScoreChanged);
        private void OnScoreChanged() => label.Text = AllServices.ScoreService.Score.ToString("D6");
    }
}
