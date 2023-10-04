using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Main
{
    public class MainScene : MonoBehaviour
    {
        private ICoinService _coinService;

        private void Awake()
        {
            _coinService = ServiceLocator.Current.Get<ICoinService>();
        }
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
            _coinService.Reset();
            Services.PlayerService.Reset();
            Services.ScoreService.Reset();
        }
    }
}