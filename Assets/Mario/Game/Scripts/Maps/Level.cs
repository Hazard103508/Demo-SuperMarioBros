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

        [SerializeField] private PlayerController _playerController;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _pauseService = ServiceLocator.Current.Get<IPauseService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();

            _playerService.SetPlayer(_playerController);
            _levelService.LoadLevel();
        }
        private void OnDestroy()
        {
            _levelService.UnloadLevel();
        }
        #endregion

        #region Input System Methods
        public void OnStart(InputValue value)
        {
            if (!value.isPressed)
                return;

            if (_pauseService.IsPaused)
                _pauseService.Resume();
            else
                _pauseService.Pause();
        }
        #endregion
    }
}