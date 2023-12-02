using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Items.Flower
{
    public abstract class FlowerState :
        IState,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight
    {
        #region Objects
        private readonly IScoreService _scoreService;
        #endregion

        #region Properties
        protected Flower Flower { get; private set; }
        #endregion

        #region Constructor
        public FlowerState(Flower flower)
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            this.Flower = flower;
        }
        #endregion

        #region State Machine
        public virtual void Enter()
        {
        }
        public virtual void Exit()
        {
        }
        public virtual void Update()
        {
        }
        #endregion

        #region On Player Hit
        public virtual void OnHittedByPlayerFromTop(PlayerController player)
        {
        }
        public virtual void OnHittedByPlayerFromBottom(PlayerController player)
        {
        }
        public virtual void OnHittedByPlayerFromLeft(PlayerController player)
        {
        }
        public virtual void OnHittedByPlayerFromRight(PlayerController player)
        {
        }
        #endregion

        #region Public Methods
        public virtual void OnGameUnfrozen() { }
        public virtual void OnGameFrozen() { }
        #endregion

        #region Protected
        protected virtual void CollectFlower(PlayerController player)
        {
            Flower.gameObject.layer = 0;
            _scoreService.Add(Flower.Profile.Points);
            _scoreService.ShowPoints(Flower.Profile.Points, Flower.transform.position + Vector3.up * 1.75f, 0.8f, 3f);

            player.Buff();
            Flower.gameObject.SetActive(false);
        }
        #endregion
    }
}
