using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects;
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

        private IGameDataService _gameDataService;
        private ICoinService _coinService;
        private IScoreService _scoreService;

        private void Awake()
        {
            _gameDataService = ServiceLocator.Current.Get<IGameDataService>();
            _coinService = ServiceLocator.Current.Get<ICoinService>();
            _scoreService = ServiceLocator.Current.Get<IScoreService>();

            labelPlayer.Text = gameDataProfile.Player;
            labelWorldName.Text = gameDataProfile.WorldMapProfile.WorldName;
        }
        private void Start()
        {
            OnScoreChanged();
            OnTimeChanged();
        }
        private void OnEnable()
        {
            _scoreService.OnScoreChanged.AddListener(OnScoreChanged);
            _coinService.OnCoinsChanged.AddListener(OnCoinsChanged);
            gameDataProfile.onTimeChanged.AddListener(OnTimeChanged);
        }
        private void OnDisable()
        {
            _scoreService.OnScoreChanged.RemoveListener(OnScoreChanged);
            _coinService.OnCoinsChanged.RemoveListener(OnCoinsChanged);
            gameDataProfile.onTimeChanged.RemoveListener(OnTimeChanged);
        }

        private void OnScoreChanged() => labelScore.Text = _gameDataService.Score.ToString("D6");
        private void OnCoinsChanged() => labelCoins.Text = _gameDataService.Coins.ToString("D2");
        private void OnTimeChanged() => labelTime.Text = gameDataProfile.Timer.ToString("D3");
    }
}
