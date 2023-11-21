using Mario.Application.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mario.Application.Services
{
    public delegate void InputActionDelegate();
    public delegate void InputActionDelegate<T>(T value);

    public class InputService : MonoBehaviour, IInputService
    {
        [SerializeField] private PlayerInput _playerInput;

        private Dictionary<string, InputActionMap> _inputMaps;

        public event InputActionDelegate StartPressed;
        public event InputActionDelegate PausePressed;
        public event InputActionDelegate<float> MovePressed;
        public event InputActionDelegate<bool> JumpPressed;
        public event InputActionDelegate<bool> SprintPressed;
        public event InputActionDelegate<bool> DuckPressed;
        public event InputActionDelegate<bool> FirePressed;

        public void Initalize()
        {
            _inputMaps = new Dictionary<string, InputActionMap>();
            foreach (var map in _playerInput.actions.actionMaps)
                _inputMaps.Add(map.name, map);
        }
        public void Dispose()
        {
            _inputMaps.Clear();
        }
        public void UseUIMap() => _playerInput.currentActionMap = _inputMaps["UI"];
        public void UseGameplayMap() => _playerInput.currentActionMap = _inputMaps["GamePlay"];

        public void OnStart() => StartPressed?.Invoke();
        public void OnPause() => PausePressed?.Invoke();
        public void OnMove(InputValue value) => MovePressed?.Invoke(value.Get<float>());
        public void OnJump(InputValue value) => JumpPressed?.Invoke(value.isPressed);
        public void OnSprint(InputValue value) => SprintPressed?.Invoke(value.isPressed);
        public void OnDuck(InputValue value) => DuckPressed?.Invoke(value.isPressed);
        public void OnFire(InputValue value) => FirePressed?.Invoke(value.isPressed);
    }
}