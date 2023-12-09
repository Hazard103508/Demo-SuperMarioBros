using Mario.Commons.Structs;
using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes.Box
{
    public class Box : MonoBehaviour,
        IHittableByMovingToTop,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight
    {
        #region Objects
        [SerializeField] private BoxProfile _profile;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _renderer;
        #endregion

        #region Properties
        public BoxStateMachine StateMachine { get; private set; }
        public Movable Movable { get; private set; }
        public Animator Animator => _animator;
        public BoxProfile Profile => _profile;
        public SpriteRenderer Renderer => _renderer;
        public bool IsLastJump { get; set; }
        #endregion

        #region Unity Methods
        protected virtual void Awake()
        {
            this.StateMachine = new BoxStateMachine(this);
            this.Movable = GetComponent<Movable>();
        }
        private void Start()
        {
            this.StateMachine.Initialize(this.StateMachine.StateIdle);
        }
        protected virtual void Update()
        {
            this.StateMachine.Update();
        }
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToTop(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToTop(hitInfo);
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromTop(player);
        public void OnHittedByPlayerFromBottom(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromBottom(player);
        public void OnHittedByPlayerFromLeft(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromLeft(player);
        public void OnHittedByPlayerFromRight(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromRight(player);
        #endregion
    }
}