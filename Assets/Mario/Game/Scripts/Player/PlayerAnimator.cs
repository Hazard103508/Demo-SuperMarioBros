using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private Dictionary<PlayerModes, PlayerAnimationMode> _playerAnimationModes;
        private PlayerController _player;
        private PlayerAnimationStates _state;
        private PlayerModes _mode;

        public PlayerAnimationStates State
        {
            get => _state;
            set
            {
                if (_state == value)
                    return;

                _state = value;

                var _currentAnimationMode = _playerAnimationModes[_mode];
                var hashId =
                    State == PlayerAnimationStates.Jumping ? _currentAnimationMode.HashIdJump :
                    State == PlayerAnimationStates.StoppingRun ? _currentAnimationMode.HashIdStop :
                    State == PlayerAnimationStates.Running ? _currentAnimationMode.HashIdRun :
                    State == PlayerAnimationStates.PowerUp ? _currentAnimationMode.HashIdPowerUp :
                    _currentAnimationMode.HashIdIdle;

                _animator.CrossFade(hashId, 0, 0);
            }
        }

        void Awake()
        {
            _playerAnimationModes = new Dictionary<PlayerModes, PlayerAnimationMode>()
            {
                [PlayerModes.Small] = new PlayerAnimationModeSmall(),
                [PlayerModes.Big] = new PlayerAnimationModeBig(),
                [PlayerModes.Super] = new PlayerAnimationModeSuper()
            };

            State = PlayerAnimationStates.Idle;

            _player = GetComponentInParent<PlayerController>();
            _animator = GetComponent<Animator>();
            _mode = _player.Mode;
        }
        void Update()
        {
            if (_player == null) return;

            if (State == PlayerAnimationStates.PowerUp)
                return;

            if (_mode != _player.Mode)
            {
                State = _mode < _player.Mode ? PlayerAnimationStates.PowerUp : PlayerAnimationStates.PowerDown;
                _mode = _player.Mode;
                return;
            }

            if (this.State != PlayerAnimationStates.Jumping)
            {
                if (_player.Input.X != 0)
                {
                    this.State = _player.RawMovement.x != 0 && Mathf.Sign(_player.RawMovement.x) != Mathf.Sign(_player.Input.X) ? PlayerAnimationStates.StoppingRun : PlayerAnimationStates.Running;
                    transform.localScale = new Vector3(_player.Input.X > 0 ? 1 : -1, 1, 1);
                }
                else
                    State = _player.RawMovement.x != 0 ? PlayerAnimationStates.Running : PlayerAnimationStates.Idle;
            }

            if (this.State == PlayerAnimationStates.Running)
                _animator.speed = _player.RawMovement.y < 0 ? 0 : Mathf.Clamp(_player.WalkSpeedFactor, 0.5f, 1.5f);

            if (_player.RawMovement.y > 0)
                this.State = PlayerAnimationStates.Jumping;

            if (_player.RawMovement.y == 0 && this.State == PlayerAnimationStates.Jumping && _player.IsGrounded)
                this.State = PlayerAnimationStates.Idle;
        }

        public void OnPowerUpCompleted()
        {
            ServiceLocator.Current.Get<ITimeService>().Enabled = true;
            State = PlayerAnimationStates.Idle;
        }
    }
}