using Mario.Game.ScriptableObjects;
using Mario.Services;
using Mario.Services.Interfaces;
using UnityEngine;

namespace Mario.Game.Handlers
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private GameDataProfile gameDataProfile;
        [SerializeField] private Mario.Game.UI.TextGenerator labelPlayer;
        [SerializeField] private Mario.Game.UI.TextGenerator labelScore;
        [SerializeField] private Mario.Game.UI.TextGenerator labelCoins;
        [SerializeField] private Mario.Game.UI.TextGenerator labelWorldName;
        [SerializeField] private Mario.Game.UI.TextGenerator labelTime;

        private void Awake()
        {
            labelPlayer.Text = gameDataProfile.Player;
            labelWorldName.Text = gameDataProfile.WorldMapProfile.WorldName;
        }
        private void Start()
        {
            OnTimeChanged();
        }
        private void OnEnable()
        {
            gameDataProfile.onScoreChanged.AddListener(OnScoreChanged);
            ServiceLocator.Current.Get<ICoinService>().OnCoinsChanged.AddListener(OnCoinsChanged);
            gameDataProfile.onTimeChanged.AddListener(OnTimeChanged);
        }
        private void OnDisable()
        {
            gameDataProfile.onScoreChanged.RemoveListener(OnScoreChanged);
            ServiceLocator.Current.Get<ICoinService>().OnCoinsChanged.RemoveListener(OnCoinsChanged);
            gameDataProfile.onTimeChanged.RemoveListener(OnTimeChanged);
        }

        private void OnScoreChanged() => labelScore.Text = gameDataProfile.Score.ToString("D6");
        private void OnCoinsChanged() => labelCoins.Text = gameDataProfile.Coins.ToString("D2");
        private void OnTimeChanged() => labelTime.Text = gameDataProfile.Timer.ToString("D3");
    }
}
