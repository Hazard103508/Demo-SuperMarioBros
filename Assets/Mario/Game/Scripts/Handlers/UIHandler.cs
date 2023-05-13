using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects;
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

        private void Start()
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

        private void OnScoreChanged() => labelScore.Text = AllServices.GameDataService.Score.ToString("D6");
        private void OnCoinsChanged() => labelCoins.Text = AllServices.GameDataService.Coins.ToString("D2");
        private void OnTimeChanged() => labelTime.Text = AllServices.GameDataService.Time.ToString("D3");
    }
}
