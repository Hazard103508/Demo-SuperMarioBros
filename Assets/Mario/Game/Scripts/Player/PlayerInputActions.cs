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
        #endregion

        #region Properties
        public Vector2 Move { get; private set; }
        public bool Jump { get; private set; }
        public bool Sprint { get; private set; }
        public bool Ducking { get; private set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
        }
        #endregion

        #region Input System Methods
        public void OnMove(InputValue value) => Move = _playerService.IsAutowalk ? Vector2.right : value.Get<Vector2>();
        public void OnJump(InputValue value) => Jump = !_playerService.IsAutowalk && value.isPressed;
        public void OnSprint(InputValue value) => Sprint = !_playerService.IsAutowalk && value.isPressed;
        public void OnDuck(InputValue value) => Ducking = !_playerService.IsAutowalk && value.isPressed;
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