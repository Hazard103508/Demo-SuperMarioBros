using Mario.Game.Interfaces;
using Mario.Game.Player;
using System;
using UnityEngine;
using UnityShared.Commons.Structs;

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
        #region Properties
        protected Star Star { get; private set; }
        #endregion

        #region Constructor
        public StarState(Star star)
        {
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
