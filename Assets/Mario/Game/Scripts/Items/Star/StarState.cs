using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Commons.Structs;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using System;
using UnityEngine;

namespace Mario.Game.Items.Star
{
    public abstract class StarState :
        IState,
        IHittableByMovingToTop,
        IHittableByMovingToBottom,
        IHittableByMovingToLeft,
        IHittableByMovingToRight,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight
    {
        #region Objects
        private readonly IScoreService _scoreService;
        #endregion

        #region Properties
        protected Star Star { get; private set; }
        #endregion

        #region Constructor
        public StarState(Star star)
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            this.Star = star;
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

        #region Public Methods
        public virtual void OnGameUnfrozen() => Star.Movable.enabled = true;
        public virtual void OnGameFrozen() => Star.Movable.enabled = false;
        #endregion

        #region Protected Methods
        protected void ChangeDirectionToRight() => Star.Movable.Speed = Mathf.Abs(Star.Movable.Speed);
        protected void ChangeDirectionToLeft() => Star.Movable.Speed = -Mathf.Abs(Star.Movable.Speed);
        protected void ChangeSpeedAfferHit(Vector3 hitPosition)
        {
            if (Math.Sign(Star.Movable.Speed) != Math.Sign(Star.transform.position.x - hitPosition.x))
                Star.Movable.Speed *= -1;
        }
        protected virtual bool CollectStar(PlayerController player)
        {
            if (!Star.gameObject.activeSelf)
                return false;

            Star.gameObject.layer = 0;
            Star.gameObject.SetActive(false);

            _scoreService.Add(Star.Profile.Points);
            _scoreService.ShowPoints(Star.Profile.Points, Star.transform.position + Vector3.up * 1.75f, 0.8f, 3f);
            player.ActivateSuperStar();

            return true;
        }
        #endregion

        #region On Movable Hit
        public virtual void OnHittedByMovingToTop(RayHitInfo hitInfo)
        {
        }
        public virtual void OnHittedByMovingToBottom(RayHitInfo hitInfo)
        {
        }
        public virtual void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
        }
        public virtual void OnHittedByMovingToRight(RayHitInfo hitInfo)
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
    }
}
