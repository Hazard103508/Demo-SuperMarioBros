using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Interactable;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Npc.Plant
{
    public class Plant : MonoBehaviour,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByFireBall
    {
        #region Objects
        private IGameplayService _gameplayService;

        [SerializeField] private PlantProfile _profile;
        #endregion

        #region Properties
        public PlantStateMachine StateMachine { get; private set; }
        #endregion

        #region Unity Methods
        protected virtual void Awake()
        {
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
            this.StateMachine = new PlantStateMachine(this);
        }
        private void Start()
        {
            this.StateMachine.Initialize(this.StateMachine.StateIdle);
        }
        protected virtual void Update()
        {
            this.StateMachine.Update();
        }
        private void OnEnable()
        {
            _gameplayService.GameFreezed += GameplayService_GameFreezed;
            _gameplayService.GameUnfreezed += GameplayService_GameUnfreezed;

            //if (this.StateMachine.CurrentState == this.StateMachine.StateWalk)
            //    this.StateMachine.CurrentState.Enter();
            //else
            //    this.StateMachine.TransitionTo(this.StateMachine.StateWalk);
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
        private void GameplayService_GameUnfreezed() => enabled = true;
        private void GameplayService_GameFreezed() => enabled = false;
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