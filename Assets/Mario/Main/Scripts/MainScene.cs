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
        private IInputService _inputService;
        private ILevelService _levelService;

        private void Awake()
        {
            _coinService = ServiceLocator.Current.Get<ICoinService>();
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _inputService = ServiceLocator.Current.Get<IInputService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();

            _inputService.UseUIMap();
        }
        private void Start()
        {
            ResetPlayerData();
        }
        private void OnEnable()
        {
            _inputService.StartPressed += InputService_StartPressed; ;
        }
        private void OnDisable()
        {
            _inputService.StartPressed -= InputService_StartPressed;
        }
        private void ResetPlayerData()
        {
            _coinService.Reset();
            _playerService.Reset();
            _scoreService.Reset();
            _levelService.Reset();
        }

        private void InputService_StartPressed() => _sceneService.LoadGameScene();
    }
}