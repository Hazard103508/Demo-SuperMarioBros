using Mario.Application.Interfaces;
using Mario.Game.Commons;
using Mario.Game.Interactable;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Player;
using Mario.Game.ScriptableObjects.Pool;
using System;
using UnityEngine;

namespace Mario.Application.Services
{
    public class PlayerService : MonoBehaviour, IPlayerService
    {
        #region Objects
        private ISoundService _soundService;
        private IPoolService _poolService;

        [SerializeField] private PlayerProfile _playerProfile;
        [SerializeField] private PooledSoundProfile _1UpSoundPoolReference;
        [SerializeField] private PooledSoundProfile _deadSoundPoolReference;
        private PlayerController _playerController;
        private PlayerInputActions _playerInputActions;
        private Movable _playerMovable;
        private int _bulletCount;
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
            _poolService = ServiceLocator.Current.Get<IPoolService>();

            Reset();
        }
        public void Dispose()
        {
        }
        public void SetPlayer(PlayerController playerController)
        {
            _playerController = playerController;
            _playerInputActions = _playerController.GetComponent<PlayerInputActions>();
            _playerMovable = _playerController.GetComponent<Movable>();
        }
        public void SetPlayerPosition(Vector3 position) => _playerController.transform.position = position;
        public void SetActivePlayer(bool isActive) => _playerController.gameObject.SetActive(isActive);
        public void EnablePlayerCollision(bool enable) => _playerMovable.ChekCollisions = enable;
        public void EnableAutoWalk(bool enable) => IsAutowalk = enable;
        public void EnableInputs(bool enable) => _playerInputActions.enabled = enable;
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
        public void ReturnFireball() => _bulletCount--;
        public bool CanShootFireball() => _bulletCount < 2;
        public void ShootFireball()
        {
            if (_bulletCount >= 2)
                return;

            float x;
            float y = _playerController.transform.position.y + PlayerProfile.Fireball.StartLocalPosition.y;
            if (_playerController.Renderer.flipX)
                x = _playerController.Renderer.transform.position.x - PlayerProfile.Fireball.StartLocalPosition.x;
            else
                x = _playerController.Renderer.transform.position.x + PlayerProfile.Fireball.StartLocalPosition.x;

            var fireBall = _poolService.GetObjectFromPool<Fireball>(PlayerProfile.Fireball.FireballPoolProfile, new Vector2(x, y));
            if (_playerController.Renderer.flipX)
                fireBall.ChangeDirectionToLeft();
            else
                fireBall.ChangeDirectionToRight();

            fireBall.Movable.enabled = true;
            _bulletCount++;
            return;
        }
        public bool IsPlayerSmall() => _playerController.StateMachine.CurrentMode.Equals(_playerController.StateMachine.ModeSmall);
        public bool IsPlayerBig() => _playerController.StateMachine.CurrentMode.Equals(_playerController.StateMachine.ModeBig);
        public bool IsPlayerSuper() => _playerController.StateMachine.CurrentMode.Equals(_playerController.StateMachine.ModeSuper);
        public bool IsPlayerNearX(float x, float distance)
        {
            var difPos = Math.Abs(x - _playerController.transform.position.x);
            return difPos < distance;
        }
        public bool IsStateJumping() => _playerController.StateMachine.CurrentState is PlayerStateJump;
        #endregion
    }
}