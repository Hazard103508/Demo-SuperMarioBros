using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items.Flower
{
    public class Flower : MonoBehaviour,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight
    {
        #region Objects
        [SerializeField] protected FlowerProfile _profile;
        #endregion

        #region Properties
        public FlowerStateMachine StateMachine { get; protected set; }
        public FlowerProfile Profile => _profile;
        #endregion

        #region Unity Methods
        protected virtual void Awake()
        {
            this.StateMachine = new FlowerStateMachine(this);
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
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromTop(player);
        public void OnHittedByPlayerFromLeft(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromLeft(player);
        public void OnHittedByPlayerFromRight(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromRight(player);
        public void OnHittedByPlayerFromBottom(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromBottom(player);
        #endregion
    }
}