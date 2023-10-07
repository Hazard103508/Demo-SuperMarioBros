using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Main
{
    public class MainScene : MonoBehaviour
    {
        private ICoinService _coinService;
        private ISceneService _sceneService;
        private IScoreService _scoreService;
        private IPlayerService _playerService;

        private void Awake()
        {
            _coinService = ServiceLocator.Current.Get<ICoinService>();
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
        }
        private void Start()
        {
            ResetPlayerData();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
                _sceneService.LoadStandByScene();
        }
        private void ResetPlayerData()
        {
            _coinService.Reset();
            _playerService.Reset();
            _scoreService.Reset();
        }
    }
}