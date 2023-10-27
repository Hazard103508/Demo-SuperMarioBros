using Mario.Application.Interfaces;
using Mario.Game.Commons;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Player;
using Mario.Game.ScriptableObjects.Pool;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mario.Application.Services
{
    public class PlayerService : MonoBehaviour, IPlayerService
    {
        #region Objects
        private ISoundService _soundService;

        [SerializeField] private PlayerProfile _playerProfile;
        [SerializeField] private PooledSoundProfile _1UpSoundPoolReference;
        [SerializeField] private PooledSoundProfile _deadSoundPoolReference;
        private PlayerController _playerController;
        private Movable _playerMovable;
        private PlayerInputActions _playerInputActions;
        #endregion

        #region Properties
        public PlayerProfile PlayerProfile => _playerProfile;
        public int Lives { get; private set; }
        public bool IsAutowalk { get; private set; }
        #endregion

        #region Events
        public event Action LivesAdded;
        public event Action LivesRemoved;
        #endregion

        #region Public Methods
        public void Initalize()
        {
            _soundService = ServiceLocator.Current.Get<ISoundService>();
            Reset();
        }
        public void Dispose()
        {
        }
        public void SetPlayer(PlayerController playerController)
        {
            _playerController = playerController;
            _playerMovable = _playerController.GetComponent<Movable>();
            _playerInputActions = _playerController.GetComponent<PlayerInputActions>();
        }
        public void SetPlayerPosition(Vector3 position) => _playerController.transform.position = position;
        public void SetActivePlayer(bool isActive) => _playerController.gameObject.SetActive(isActive);
        public void EnablePlayerController(bool enable) => _playerController.enabled = enable;
        public void EnablePlayerMovable(bool enable) => _playerMovable.enabled = enable;
        public void EnableAutoWalk(bool enable)
        {
            _playerInputActions.ResetInputs();
            IsAutowalk = enable;
        }
        public void ResetState() => _playerController.StateMachine.TransitionTo(_playerController.StateMachine.CurrentMode.StateIdle);
        public void KillPlayer() => _playerController.Kill();
        public void KillPlayerByTimeOut() => _playerController.TimeOut();
        public void TranslatePlayerPosition(Vector3 position) => _playerController.transform.Translate(position);
        public void AddLife()
        {
            this.Lives++;
            _soundService.Play(_1UpSoundPoolReference);
            LivesAdded?.Invoke();
        }
        public void RemoveLife()
        {
            this.Lives--;
            _soundService.Play(_deadSoundPoolReference);
            LivesRemoved?.Invoke();
        }
        public void Reset()
        {
            Lives = 3;
        }
        #endregion
    }
}