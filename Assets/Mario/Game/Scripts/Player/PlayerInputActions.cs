using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mario.Game.Player
{
    public class PlayerInputActions : MonoBehaviour
    {
        #region Objects
        private IPlayerService _playerService;
        private IPauseService _pauseService;

        private Vector2 _move;
        private bool _jump;
        private bool _sprint;
        private bool _ducking;
        #endregion

        #region Properties
        public Vector2 Move 
        {
            get
            {
                return _pauseService.IsPaused ? Vector2.zero :
                _playerService.IsAutowalk ? Vector2.right :
                _move;
            }
            private set => _move = value; 
        }
        public bool Jump 
        {
            get => !_pauseService.IsPaused && !_playerService.IsAutowalk && _jump;
            private set => _jump = value; 
        }
        public bool Sprint
        {
            get => !_pauseService.IsPaused && !_playerService.IsAutowalk && _sprint;
            private set => _sprint = value;
        }
        public bool Ducking
        {
            get => !_pauseService.IsPaused && !_playerService.IsAutowalk && _ducking;
            private set => _ducking = value;
        }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _pauseService = ServiceLocator.Current.Get<IPauseService>();
        }
        #endregion

        #region Input System Methods
        public void OnMove(InputValue value) => Move = value.Get<Vector2>();
        public void OnJump(InputValue value) => Jump = value.isPressed;
        public void OnSprint(InputValue value) => Sprint = value.isPressed;
        public void OnDuck(InputValue value) => Ducking = value.isPressed;
        public void ResetInputs()
        {
            Move = Vector2.zero;
            Jump = false; 
            Sprint = false; 
            Ducking = false;
        }
        #endregion

    }
}