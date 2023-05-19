using Mario.Application.Services;
using Mario.Game.Enums;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        private PlayerAnimationFrames _animationFrame; // el ultimo frame me determina que animacion de debo mostrar al agarrar la flor, y asi respetar la animacion en curso

        public PlayerAnimationStates State
        {
            get => _state;
            set
            {
                if (_state == value)
                    return;

                _playerAnimationModes[_mode].ChangeAnimation(_animator, value, _animationFrame);
                _state = value;
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

            if (this.State == PlayerAnimationStates.Jumping)
            {
                if (_player.RawMovement.y == 0 && _player.IsGrounded)
                    this.State = PlayerAnimationStates.Idle;
                else
                    return;
            }

            if (this.State == PlayerAnimationStates.Ducking && _player.RawMovement.y != 0)
                return;


            if (_player.IsDucking)
            {
                this.State = PlayerAnimationStates.Ducking;
                return;
            }

            if (this.State == PlayerAnimationStates.Running)
                _animator.speed = _player.RawMovement.y < 0 ? 1 : Mathf.Clamp(_player.WalkSpeedFactor, 0.5f, 1.5f);
            

            if (this.State != PlayerAnimationStates.Jumping)
            {
                if (_player.Input.IsDucking)
                {
                    this.State = _player.RawMovement.x != 0 ? PlayerAnimationStates.Running : PlayerAnimationStates.Idle;
                    return;
                }

                if (_player.Input.X != 0)
                {
                    this.State = _player.RawMovement.x != 0 && Mathf.Sign(_player.RawMovement.x) != Mathf.Sign(_player.Input.X) ? PlayerAnimationStates.StoppingRun : PlayerAnimationStates.Running;
                    transform.localScale = new Vector3(_player.Input.X > 0 ? 1 : -1, 1, 1);
                }
                else
                    State = _player.RawMovement.x != 0 ? PlayerAnimationStates.Running : PlayerAnimationStates.Idle;
            }

            if (_player.RawMovement.y > 0)
                this.State = PlayerAnimationStates.Jumping;
        }

        public void OnPowerUpCompleted()
        {
            AllServices.TimeService.StartTimer();
            AllServices.CharacterService.ResumeMovement();

            State = PlayerAnimationStates.Idle;
        }
        public void OnPlayerAnimationFramesChanged(PlayerAnimationFrames frame) => _animationFrame = frame;
    }
}