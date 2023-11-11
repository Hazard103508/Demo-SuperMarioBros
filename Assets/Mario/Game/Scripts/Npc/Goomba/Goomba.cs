using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Commons;
using Mario.Game.Interactable;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Goomba
{
    public class Goomba : MonoBehaviour,
        IHittableByMovingToLeft,
        IHittableByMovingToRight,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByBox,
        IHittableByKoppa,
        IHittableByFireBall
    {
        #region Objects
        private IGameplayService _gameplayService;

        [SerializeField] private GoombaProfile _profile;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Animator _animator;
        #endregion

        #region Properties
        public GoombaStateMachine StateMachine { get; private set; }
        public Movable Movable { get; private set; }
        public Animator Animator => _animator;
        public GoombaProfile Profile => _profile;
        public SpriteRenderer Renderer => _renderer;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();

            this.StateMachine = new GoombaStateMachine(this);
            Movable = GetComponent<Movable>();
        }
        private void Start()
        {
            this.StateMachine.Initialize(this.StateMachine.StateWalk);
        }
        private void Update()
        {
            this.StateMachine.Update();
        }
        private void OnEnable()
        {
            _gameplayService.GameFreezed += GameplayService_GameFreezed;
            _gameplayService.GameUnfreezed += GameplayService_GameUnfreezed;

            if (this.StateMachine.CurrentState == this.StateMachine.StateWalk)
                this.StateMachine.CurrentState.Enter();
            else
                this.StateMachine.TransitionTo(this.StateMachine.StateWalk);
        }
        private void OnDisable()
        {
            _gameplayService.GameFreezed -= GameplayService_GameFreezed;
            _gameplayService.GameUnfreezed -= GameplayService_GameUnfreezed;
        }
        #endregion

        #region Public Methods
        public void OnOutOfScreen() => gameObject.SetActive(false);
        #endregion

        #region Private
        private void GameplayService_GameUnfreezed() => Movable.enabled = true;
        private void GameplayService_GameFreezed() => Movable.enabled = false;
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToLeft(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToLeft(hitInfo);
        public void OnHittedByMovingToRight(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToRight(hitInfo);
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromTop(player);
        public void OnHittedByPlayerFromLeft(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromLeft(player);
        public void OnHittedByPlayerFromRight(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromRight(player);
        public void OnHittedByPlayerFromBottom(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromBottom(player);
        #endregion

        #region On Box Hit
        public void OnHittedByBox(GameObject box) => this.StateMachine.CurrentState.OnHittedByBox(box);
        #endregion

        #region On Koopa Hit
        public void OnHittedByKoppa(Koopa.Koopa koopa) => this.StateMachine.CurrentState.OnHittedByKoppa(koopa);
        #endregion

        #region On Fireball Hit
        public void OnHittedByFireBall(Fireball fireball) => this.StateMachine.CurrentState.OnHittedByFireBall(fireball);
        #endregion
    }
}