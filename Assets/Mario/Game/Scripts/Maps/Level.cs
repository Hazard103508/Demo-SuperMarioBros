using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mario.Game.Maps
{
    public class Level : MonoBehaviour
    {
        #region Objects
        private IPauseService _pauseService;
        private ILevelService _levelService;
        private IPlayerService _playerService;
        private IInputService _inputService;

        [SerializeField] private PlayerController _playerController;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _pauseService = ServiceLocator.Current.Get<IPauseService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _inputService = ServiceLocator.Current.Get<IInputService>();

            _playerService.SetPlayer(_playerController);
            _levelService.LoadLevel();
            _inputService.UseGameplayMap();
        }
        private void OnDestroy()
        {
            _levelService.UnloadLevel();
        }
        private void OnEnable()
        {
            _inputService.PausePressed += InputService_PausePressed;
        }
        private void OnDisable()
        {
            _inputService.PausePressed -= InputService_PausePressed;
        }
        #endregion

        private void InputService_PausePressed()
        {
            Debug.Log("PAUSE");

            if (_pauseService.IsPaused)
                _pauseService.Resume();
            else
                _pauseService.Pause();
        }
    }
}