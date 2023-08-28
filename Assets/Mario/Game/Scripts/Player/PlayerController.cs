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
        [SerializeField] private PlayerProfile _profile;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Animator _animator;
        #endregion

        #region Properties
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerInputActions InputActions { get; private set; }
        public Movable Movable { get; private set; }
        public Animator Animator => _animator;
        public PlayerProfile Profile => _profile;
        public SpriteRenderer Renderer => _renderer;
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
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToTop(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToTop(hitInfo);
        public void OnHittedByMovingToBottom(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToBottom(hitInfo);
        public void OnHittedByMovingToLeft(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToLeft(hitInfo);
        public void OnHittedByMovingToRight(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToRight(hitInfo);
        #endregion
    }
}