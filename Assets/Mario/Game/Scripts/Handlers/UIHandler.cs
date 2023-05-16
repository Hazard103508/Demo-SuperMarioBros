using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Handlers
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private Mario.Game.UI.TextGenerator labelPlayer;
        [SerializeField] private Mario.Game.UI.TextGenerator labelScore;
        [SerializeField] private Mario.Game.UI.TextGenerator labelCoins;
        [SerializeField] private Mario.Game.UI.TextGenerator labelWorldName;
        [SerializeField] private Mario.Game.UI.TextGenerator labelTime;

        private void Awake()
        {
            OnScoreChanged();
            OnTimeChanged();

            labelPlayer.Text = AllServices.GameDataService.PlayerProfile.PlayerName;
            labelWorldName.Text = AllServices.GameDataService.MapProfile.WorldName;
        }
        private void OnEnable()
        {
            AllServices.ScoreService.OnScoreChanged.AddListener(OnScoreChanged);
            AllServices.CoinService.OnCoinsChanged.AddListener(OnCoinsChanged);
            AllServices.TimeService.OnTimeChanged.AddListener(OnTimeChanged);
        }
        private void OnDisable()
        {
            AllServices.ScoreService.OnScoreChanged.RemoveListener(OnScoreChanged);
            AllServices.CoinService.OnCoinsChanged.RemoveListener(OnCoinsChanged);
            AllServices.TimeService.OnTimeChanged.RemoveListener(OnTimeChanged);
        }

        private void OnScoreChanged() => labelScore.Text = AllServices.ScoreService.Score.ToString("D6");
        private void OnCoinsChanged() => labelCoins.Text = AllServices.CoinService.Coins.ToString("D2");
        private void OnTimeChanged() => labelTime.Text = AllServices.TimeService.Time.ToString("D3");
    }
}
