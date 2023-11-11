using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Player;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private Image _loadingCover;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _pauseService = ServiceLocator.Current.Get<IPauseService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _inputService = ServiceLocator.Current.Get<IInputService>();

            _levelService.LoadCompleted += OnLoadCompleted;

            _playerService.SetPlayer(_playerController);
            _levelService.LoadLevel();
            _inputService.UseGameplayMap();
        }

        private void OnDestroy()
        {
            _levelService.LoadCompleted -= OnLoadCompleted;
            _levelService.UnloadLevel();

            if (_loadingCover != null && _loadingCover.gameObject != null)
                _loadingCover.gameObject.SetActive(true);
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

        #region Private Methods
        private void InputService_PausePressed()
        {
            if (_pauseService.IsPaused)
                _pauseService.Resume();
            else
                _pauseService.Pause();
        }
        private void OnLoadCompleted()
        {
            _loadingCover.gameObject.SetActive(false);
        }
        #endregion
    }
}