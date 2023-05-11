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

        private void Awake()
        {
            labelPlayer.Text = gameDataProfile.Player;
        }
        private void OnEnable()
        {
            gameDataProfile.onScoreChanged.AddListener(OnScoreChanged);
            gameDataProfile.onCoinsChanged.AddListener(OnCoinsChanged);
        }
        private void OnDisable()
        {
            gameDataProfile.onScoreChanged.RemoveListener(OnScoreChanged);
            gameDataProfile.onCoinsChanged.RemoveListener(OnCoinsChanged);
        }

        private void OnScoreChanged() => labelScore.Text = gameDataProfile.Score.ToString("D6");
        private void OnCoinsChanged() => labelCoins.Text = gameDataProfile.Coins.ToString("D2");
    }
}
