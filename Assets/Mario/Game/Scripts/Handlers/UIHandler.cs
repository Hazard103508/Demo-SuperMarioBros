using Mario.Game.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Mario.Game.Handlers
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private GameDataProfile gameDataProfile;
        [SerializeField] private Text labelPlayer;
        [SerializeField] private Text labelScore;

        private void Awake()
        {
            labelPlayer.text = gameDataProfile.Player;
        }
        private void OnEnable()
        {
            gameDataProfile.onScoreChanged.AddListener(OnScoreChanged);
        }
        private void OnDisable()
        {
            gameDataProfile.onScoreChanged.RemoveListener(OnScoreChanged);
        }

        private void OnScoreChanged()
        {
            labelScore.text = gameDataProfile.Score.ToString("D6");
            print("score updated!");
        }
    }
}
