using Mario.Application.Services;
using Mario.Game.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mario.Game.Player
{
    //public class PlayerAnimator_OLD : MonoBehaviour
    //{
    //    [SerializeField] private SpriteRenderer _renderer;
    //
    //    private Animator _animator;
    //    private Dictionary<PlayerModes, PlayerAnimationMode> _playerAnimationModes;
    //    private PlayerController_OLD _player;
    //    private PlayerAnimationStates _state;
    //    private PlayerAnimationStates _previousState;
    //    private PlayerModes _mode;
    //    private PlayerAnimationFrames _animationFrame; // el ultimo frame me determina que animacion de debo mostrar al agarrar la flor, y asi respetar la animacion en curso
    //
    //    public bool IsInFlagPole { get; set; }
    //    public bool IsInFlagBase { get; set; }
    //    public PlayerAnimationStates State
    //    {
    //        get => _state;
    //        set
    //        {
    //            if (_state == value)
    //                return;
    //
    //            _playerAnimationModes[_mode].ChangeAnimation(_animator, value, _animationFrame);
    //            _previousState = _state;
    //            _state = value;
    //        }
    //    }
    //
    //    void Awake()
    //    {
    //        _playerAnimationModes = new Dictionary<PlayerModes, PlayerAnimationMode>()
    //        {
    //            [PlayerModes.Small] = new PlayerAnimationModeSmall(),
    //            [PlayerModes.Big] = new PlayerAnimationModeBig(),
    //            [PlayerModes.Super] = new PlayerAnimationModeSuper()
    //        };
    //
    //        _player = GetComponentInParent<PlayerController_OLD>();
    //        _animator = GetComponent<Animator>();
    //    }
    //    private void Start()
    //    {
    //        _mode = _player.Mode;
    //        State = PlayerAnimationStates.Idle;
    //    }
    //    void Update() => UpdateAnimation();
    //
    //    public void OnPlayerAnimationFramesChanged(PlayerAnimationFrames frame) => _animationFrame = frame;
    //    public void OnPlayerBuffCompleted()
    //    {
    //        Services.TimeService.StartTimer();
    //        Services.PlayerService.CanMove = true;
    //
    //        State = _previousState;
    //    }
    //    public void OnPlayerNerfComplete()
    //    {
    //        Services.TimeService.StartTimer();
    //        Services.PlayerService.CanMove = true;
    //        State = PlayerAnimationStates.Idle;
    //        StartCoroutine(StartInvensible());
    //    }
    //
    //    private void UpdateAnimation()
    //    {
    //        if (_player == null) return;
    //
    //        if (this.IsInFlagPole)
    //        {
    //            State = PlayerAnimationStates.Flag;
    //            if (this.IsInFlagBase)
    //                _animator.speed = 0;
    //            return;
    //        }
    //
    //        if (_player.IsDead)
    //        {
    //            State = PlayerAnimationStates.Died;
    //            return;
    //        }
    //
    //        if (State == PlayerAnimationStates.Buff || State == PlayerAnimationStates.Nerf)
    //            return;
    //
    //        if (_mode != _player.Mode)
    //        {
    //            State = _mode < _player.Mode ? PlayerAnimationStates.Buff : PlayerAnimationStates.Nerf;
    //            _mode = _player.Mode;
    //            return;
    //        }
    //
    //        if (this.State == PlayerAnimationStates.Jumping)
    //        {
    //            if (_player.RawMovement.y == 0 && _player.IsGrounded)
    //                this.State = PlayerAnimationStates.Idle;
    //            else
    //                return;
    //        }
    //
    //        if (this.State == PlayerAnimationStates.Ducking && _player.RawMovement.y != 0)
    //            return;
    //
    //        if (_player.IsDucking)
    //        {
    //            this.State = PlayerAnimationStates.Ducking;
    //            return;
    //        }
    //
    //        if (this.State == PlayerAnimationStates.Running)
    //            _animator.speed = _player.RawMovement.y < 0 ? 0 : Mathf.Clamp(_player.WalkSpeedFactor, 0.5f, 1.5f);
    //        else
    //            _animator.speed = 1;
    //
    //        if (this.State != PlayerAnimationStates.Jumping)
    //        {
    //            if (_player.Input.IsDucking && _player.Mode != PlayerModes.Small)
    //            {
    //                this.State = _player.RawMovement.x != 0 ? PlayerAnimationStates.Running : PlayerAnimationStates.Idle;
    //                return;
    //            }
    //
    //            if (_player.Input.X != 0)
    //            {
    //                this.State = _player.RawMovement.x != 0 && Mathf.Sign(_player.RawMovement.x) != Mathf.Sign(_player.Input.X) ? PlayerAnimationStates.StoppingRun : PlayerAnimationStates.Running;
    //                transform.localScale = new Vector3(_player.Input.X > 0 ? 1 : -1, 1, 1);
    //            }
    //            else if (_player.IsGrounded)
    //                State = _player.RawMovement.x != 0 ? PlayerAnimationStates.Running : PlayerAnimationStates.Idle;
    //        }
    //
    //        if (_player.RawMovement.y > 0)
    //            this.State = PlayerAnimationStates.Jumping;
    //    }
    //    private IEnumerator StartInvensible()
    //    {
    //        yield return new WaitForEndOfFrame();
    //        _player.RefreshCollider();
    //
    //        Color visible = Color.white;
    //        Color invisible = new Color(0, 0, 0, 0);
    //
    //        float _intervalTime = 0.05f;
    //        float _intervalCount = 2.5f / _intervalTime;
    //
    //        for (int i = 0; i < _intervalCount; i++)
    //        {
    //            _renderer.color = i % 2 == 0 ? visible : invisible;
    //            yield return new WaitForSeconds(_intervalTime);
    //        }
    //
    //        _renderer.color = visible;
    //        _player.IsInvensible = false;
    //    }
    //}
}