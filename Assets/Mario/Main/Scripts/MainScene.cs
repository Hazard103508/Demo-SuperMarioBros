using Mario.Application.Services;
using UnityEngine;

namespace Mario.Main
{
    public class MainScene : MonoBehaviour
    {
        private void Start()
        {
            ResetPlayerData();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
                Services.SceneService.LoadStandByScene();
        }
        private void ResetPlayerData()
        {
            Services.CoinService.Reset();
            Services.PlayerService.Reset();
            Services.ScoreService.Reset();
        }
    }
}