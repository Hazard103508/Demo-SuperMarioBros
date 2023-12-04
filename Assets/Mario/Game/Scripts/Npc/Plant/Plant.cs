using Mario.Game.Interactable;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Npc.Plant
{
    public class Plant : MonoBehaviour,
        IEnemy,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByFireBall
    {
        #region Objects
        [SerializeField] private PlantProfile _profile;
        #endregion

        #region Properties
        public PlantStateMachine StateMachine { get; private set; }
        public PlantProfile Profile => _profile;
        #endregion

        #region Unity Methods
        protected virtual void Awake()
        {
            this.StateMachine = new PlantStateMachine(this);
        }
        private void Start()
        {
            this.StateMachine.Initialize(this.StateMachine.StateRising);
        }
        protected virtual void Update()
        {
            this.StateMachine.Update();
        }
        private void OnEnable()
        {
            if (this.StateMachine.CurrentState == this.StateMachine.StateRising)
                this.StateMachine.CurrentState.Enter();
            else
                this.StateMachine.TransitionTo(this.StateMachine.StateRising);
        }
        #endregion

        #region Public Methods
        public void Kill(Vector3 hitPosition) => this.StateMachine.CurrentState.Kill(hitPosition);
        public void OnOutOfScreen() => gameObject.SetActive(false);
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromTop(player);
        public void OnHittedByPlayerFromLeft(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromLeft(player);
        public void OnHittedByPlayerFromRight(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromRight(player);
        #endregion

        #region On Fireball Hit
        public void OnHittedByFireBall(Fireball fireball) => this.StateMachine.CurrentState.OnHittedByFireBall(fireball);
        #endregion
    }
}