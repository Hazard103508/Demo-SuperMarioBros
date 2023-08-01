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
            if(Input.GetKeyDown(KeyCode.Return))
                AllServices.SceneService.LoadStandByScene();
        }
        private void ResetPlayerData()
        {
            AllServices.CoinService.Reset();
            AllServices.PlayerService.Reset();
            AllServices.ScoreService.Reset();
        }
    }
}