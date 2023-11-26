using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Items.Star
{
    public class Star : MonoBehaviour,
        IHittableByMovingToBottom,
        IHittableByMovingToLeft,
        IHittableByMovingToRight,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight
    {
        #region Objects
        [SerializeField] private StarProfile _profile;
        #endregion

        #region Properties
        public StarStateMachine StateMachine { get; protected set; }
        public Movable Movable { get; private set; }
        public StarProfile Profile => _profile;
        #endregion

        #region Unity Methods
        protected virtual void Awake()
        {
            this.StateMachine = new StarStateMachine(this);
            Movable = GetComponent<Movable>();
        }
        private void Start()
        {
            this.StateMachine.Initialize(this.StateMachine.StateRising);
        }
        private void Update()
        {
            this.StateMachine.Update();
        }
        private void OnEnable()
        {
            this.StateMachine.TransitionTo(this.StateMachine.StateRising);
            Movable.Speed = 0;
        }
        #endregion

        #region Public Methods
        public void OnOutOfScreen() => gameObject.SetActive(false);
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToBottom(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToBottom(hitInfo);
        public void OnHittedByMovingToLeft(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToLeft(hitInfo);
        public void OnHittedByMovingToRight(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToRight(hitInfo);
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromTop(player);
        public void OnHittedByPlayerFromLeft(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromLeft(player);
        public void OnHittedByPlayerFromRight(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromRight(player);
        public void OnHittedByPlayerFromBottom(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromBottom(player);
        #endregion
    }
}