using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.ScriptableObjects.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerController : MonoBehaviour,
        IHittableByMovingToTop,
        IHittableByMovingToBottom,
        IHittableByMovingToLeft,
        IHittableByMovingToRight
    {
        #region Objects
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Animator _animator;
        #endregion

        #region Properties
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerInputActions InputActions { get; private set; }
        public Movable Movable { get; private set; }
        public Animator Animator => _animator;
        public SpriteRenderer Renderer => _renderer;
        public bool IsInvincible { get; set; }
        public PlayerAnimator.PlayerAnimationKeys CurrentAnimationKey { get; set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            this.StateMachine = new PlayerStateMachine(this);
            this.InputActions = GetComponent<PlayerInputActions>();
            this.Movable = GetComponent<Movable>();
        }
        private void Start()
        {
            this.StateMachine.Initialize(this.StateMachine.CurrentMode.StateIdle);
        }
        private void Update()
        {
            this.StateMachine.Update();
        }
        #endregion

        #region Public Methods
        public void Buff() => this.StateMachine.CurrentState.OnBuff();
        public void Nerf() => this.StateMachine.CurrentState.OnNerf();
        public void Kill() => this.StateMachine.CurrentState.OnDeath();
        public void TimeOut() => this.StateMachine.CurrentState.OnTimeOut();
        public void TouchFlag() => this.StateMachine.CurrentState.OnTouchFlag();
        public void Hit()
        {
            if (StateMachine.CurrentMode == StateMachine.ModeSmall)
                Kill();
            else
                Nerf();
        }
        //---Temp---
        public void BounceJump() { }
        public bool IsAutoWalk { get; set; }
        //---Temp---
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToTop(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToTop(hitInfo);
        public void OnHittedByMovingToBottom(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToBottom(hitInfo);
        public void OnHittedByMovingToLeft(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToLeft(hitInfo);
        public void OnHittedByMovingToRight(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToRight(hitInfo);
        #endregion
    }
}