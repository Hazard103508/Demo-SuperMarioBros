using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Commons.Structs;
using Mario.Game.Commons;
using Mario.Game.Player;
using UnityEngine;
using UnityEngine.UI;
using static Mario.Application.Services.LevelService;

namespace Mario.Game.Maps
{
    public class Level : MonoBehaviour
    {
        #region Objects
        private IPauseService _pauseService;
        private ILevelService _levelService;
        private IPlayerService _playerService;
        private IInputService _inputService;

        [SerializeField] private LockCameraX _lockCameraX;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Image _loadingCover;
        [SerializeField] private GameObject _standBy;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _pauseService = ServiceLocator.Current.Get<IPauseService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _inputService = ServiceLocator.Current.Get<IInputService>();

            _inputService.PausePressed += InputService_PausePressed;
            _levelService.StartLoading += OnLevelStartLoading;
            _levelService.LoadCompleted += OnLevelLoadCompleted;

            _playerService.SetPlayer(_playerController);
            _levelService.LoadLevel(true);
            _inputService.UseGameplayMap();
        }
        private void OnDestroy()
        {
            _inputService.PausePressed -= InputService_PausePressed;
            _levelService.StartLoading -= OnLevelStartLoading;
            _levelService.LoadCompleted -= OnLevelLoadCompleted;

            _levelService.UnloadLevel();

            if (_loadingCover != null && _loadingCover.gameObject != null)
                _loadingCover.gameObject.SetActive(true);
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
        private void OnLevelStartLoading(StartLoadingEvent arg)
        {
            _standBy.gameObject.SetActive(arg.ShowStandby);
            _loadingCover.gameObject.SetActive(true);
        }
        private void OnLevelLoadCompleted()
        {
            _standBy.gameObject.SetActive(false);
            _loadingCover.gameObject.SetActive(false);

            CenterCamera();
        }
        private void CenterCamera()
        {
            var map = _levelService.MapProfile;

            float _min = 8f;
            float _max = Mathf.Max(_min, map.Width - _min);
            _lockCameraX.XPosition = new RangeNumber<float>(_min, _max);
        }
        #endregion
    }
}