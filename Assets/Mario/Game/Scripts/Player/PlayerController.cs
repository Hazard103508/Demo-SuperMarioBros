using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Commons.Structs;
using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.Npc.Koopa;
using System.Collections;
using UnityEngine;
using static Mario.Application.Services.LevelService;

namespace Mario.Game.Player
{
    public class PlayerController : MonoBehaviour,
        IHittableByMovingToTop,
        IHittableByMovingToBottom,
        IHittableByMovingToLeft,
        IHittableByMovingToRight,
        IHittableByKoppa
    {
        #region Objects
        private IGameplayService _gameplayService;
        private ILevelService _levelService;

        private Coroutine _invincibleCO;

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private BoxCollider2D _boxCollider;
        #endregion

        #region Properties
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerInputActions InputActions { get; private set; }
        public Movable Movable { get; private set; }
        public Animator Animator => _animator;
        public SpriteRenderer Renderer => _renderer;
        public BoxCollider2D Collider => _boxCollider;
        public bool IsInvincible { get; set; }
        public PlayerAnimator.PlayerAnimationKeys CurrentAnimationKey { get; set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
            _gameplayService.GameFrozen += GameplayService_GameFrozen;
            _gameplayService.GameUnfrozen += GameplayService_GameUnfrozen;

            _levelService = ServiceLocator.Current.Get<ILevelService>();
            _levelService.StartLoading += LevelService_StartLoading;

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
            if (_gameplayService.IsFrozen)
                return;

            this.StateMachine.Update();
        }
        private void OnDestroy()
        {
            _gameplayService.GameFrozen -= GameplayService_GameFrozen;
            _gameplayService.GameUnfrozen -= GameplayService_GameUnfrozen;
            _levelService.StartLoading -= LevelService_StartLoading;
        }
        private void OnEnable()
        {
            Renderer.sortingLayerName = "Player";
        }
        #endregion

        #region Public Methods
        public void ActivateSuperStar() => this.StateMachine.CurrentState.OnSuperStarActivated();
        public void Buff() => this.StateMachine.CurrentState.OnBuff();
        public void Nerf() => this.StateMachine.CurrentState.OnNerf();
        public void Kill() => this.StateMachine.CurrentState.OnDeath();
        public void TimeOut() => this.StateMachine.CurrentState.OnTimeOut();
        public void TouchFlag() => this.StateMachine.CurrentState.OnTouchFlag();
        public void Hit(IEnemy npc)
        {
            if (_gameplayService.IsStarman)
            {
                npc.Kill(transform.position);
                return;
            }

            if (StateMachine.CurrentMode == StateMachine.ModeSmall)
                Kill();
            else
                Nerf();
        }
        public void OnFall() => this.StateMachine.CurrentState.OnFall();
        public void BounceJump() => this.StateMachine.CurrentState.OnBounceJump();
        public void SetInvincible() => _invincibleCO = StartCoroutine(SetInvincibleCO());
        #endregion

        #region Private Methods
        private void GameplayService_GameUnfrozen() => this.StateMachine.CurrentState.OnGameUnfrozen();
        private void GameplayService_GameFrozen() => this.StateMachine.CurrentState.OnGameFrozen();
        private void LevelService_StartLoading(StartLoadingEvent arg)
        {
            if (_invincibleCO != null)
            {
                StopCoroutine(_invincibleCO);
                Renderer.color = Color.white;
            }
            CurrentAnimationKey = PlayerAnimator.PlayerAnimationKeys.Run1;
        }
        private IEnumerator SetInvincibleCO()
        {
            IsInvincible = true;
            yield return new WaitForEndOfFrame();

            float _intervalTime = 0.05f;
            float _intervalCount = 2.5f / _intervalTime;

            var _colorEnabled = Color.white;
            var _colorDisable = new Color(0, 0, 0, 0);
            for (int i = 0; i < _intervalCount; i++)
            {
                Renderer.color = i % 2 == 0 ? _colorEnabled : _colorDisable;
                yield return new WaitForSeconds(_intervalTime);
            }

            Renderer.color = _colorEnabled;
            IsInvincible = false;
        }
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToTop(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToTop(hitInfo);
        public void OnHittedByMovingToBottom(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToBottom(hitInfo);
        public void OnHittedByMovingToLeft(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToLeft(hitInfo);
        public void OnHittedByMovingToRight(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToRight(hitInfo);
        #endregion

        #region On Koopa Hit
        public void OnHittedByKoppa(Koopa koopa) => this.StateMachine.CurrentState.OnHittedByKoppa(koopa);
        #endregion
    }
}