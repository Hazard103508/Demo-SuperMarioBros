using Mario.Application.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mario.Application.Services
{
    public delegate void InputActionDelegate();

    public class InputService : MonoBehaviour, IInputService
    {
        [SerializeField] private PlayerInput _playerInput;

        private Dictionary<string, InputActionMap> _inputMaps;


        public event InputActionDelegate StartPressed;
        public event InputActionDelegate PausePressed;


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

        public void OnStart(InputValue value) => StartPressed?.Invoke();
        public void OnPause(InputValue value) => PausePressed?.Invoke();
    }
}