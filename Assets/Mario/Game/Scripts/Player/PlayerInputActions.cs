using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerInputActions : MonoBehaviour
    {
        #region Objects
        private IGameplayService _gameplayService;
        private IPlayerService _playerService;
        private IPauseService _pauseService;
        private IInputService _inputService;

        private float _move;
        private bool _jump;
        private bool _sprint;
        private bool _ducking;
        private bool _fire;
        #endregion

        #region Properties
        public float Move
        {
            get
            {
                return
                    _gameplayService.State != GameplayService.GameState.Play ? 0 :
                    !enabled ? 0 :
                    _pauseService.IsPaused ? 0 :
                    _playerService.IsAutowalk ? 1 :
                    _move;
            }
            private set => _move = value;
        }
        public bool Jump
        {
            get => _gameplayService.State == GameplayService.GameState.Play && enabled && !_pauseService.IsPaused && !_playerService.IsAutowalk && _jump;
            private set => _jump = value;
        }
        public bool Sprint
        {
            get => _gameplayService.State == GameplayService.GameState.Play && enabled && !_pauseService.IsPaused && !_playerService.IsAutowalk && _sprint;
            private set => _sprint = value;
        }
        public bool Ducking
        {
            get => _gameplayService.State == GameplayService.GameState.Play && enabled && !_pauseService.IsPaused && !_playerService.IsAutowalk && _ducking;
            private set => _ducking = value;
        }
        public bool Fire
        {
            get => _gameplayService.State == GameplayService.GameState.Play && enabled && !_pauseService.IsPaused && !_playerService.IsAutowalk && _fire;
            private set => _fire = value;
        }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _pauseService = ServiceLocator.Current.Get<IPauseService>();
            _inputService = ServiceLocator.Current.Get<IInputService>();

            _inputService.MovePressed += InputService_MovePressed;
            _inputService.JumpPressed += InputService_JumpPressed;
            _inputService.SprintPressed += InputService_SprintPressed;
            _inputService.DuckPressed += InputService_DuckPressed;
            _inputService.FirePressed += InputService_FirePressed;
        }
        private void LateUpdate()
        {
            Fire = false;
        }
        private void OnDestroy()
        {
            _inputService.MovePressed -= InputService_MovePressed;
            _inputService.JumpPressed -= InputService_JumpPressed;
            _inputService.SprintPressed -= InputService_SprintPressed;
            _inputService.DuckPressed -= InputService_DuckPressed;
            _inputService.FirePressed -= InputService_FirePressed;
        }
        #endregion

        #region Input System Methods
        private void InputService_MovePressed(float value) => Move = value;
        private void InputService_JumpPressed(bool value) => Jump = value;
        private void InputService_DuckPressed(bool value) => Ducking = value;
        private void InputService_SprintPressed(bool value) => Sprint = value;
        private void InputService_FirePressed(bool value) => Fire = value;
        #endregion
    }
}