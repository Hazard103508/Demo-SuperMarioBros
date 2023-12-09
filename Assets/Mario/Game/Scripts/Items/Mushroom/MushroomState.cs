using Mario.Commons.Structs;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using System;
using UnityEngine;

namespace Mario.Game.Items.Mushroom
{
    public abstract class MushroomState :
        IState,
        IHittableByMovingToBottom,
        IHittableByMovingToLeft,
        IHittableByMovingToRight,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByBox
    {
        #region Properties
        protected Mushroom Mushroom { get; private set; }
        #endregion

        #region Constructor
        public MushroomState(Mushroom mushroom)
        {
            this.Mushroom = mushroom;
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
        public virtual void OnGameUnfrozen() => Mushroom.Movable.enabled = true;
        public virtual void OnGameFrozen() => Mushroom.Movable.enabled = false;
        #endregion

        #region Protected Methods
        protected void ChangeDirectionToRight() => Mushroom.Movable.Speed = Mathf.Abs(Mushroom.Movable.Speed);
        protected void ChangeDirectionToLeft() => Mushroom.Movable.Speed = -Mathf.Abs(Mushroom.Movable.Speed);
        protected void ChangeSpeedAfferHit(Vector3 hitPosition)
        {
            if (Math.Sign(Mushroom.Movable.Speed) != Math.Sign(Mushroom.transform.position.x - hitPosition.x))
                Mushroom.Movable.Speed *= -1;
        }
        protected virtual bool CollectMushroom(PlayerController player)
        {
            if (!Mushroom.gameObject.activeSelf)
                return false;

            Mushroom.gameObject.layer = 0;
            Mushroom.gameObject.SetActive(false);
            return true;
        }
        #endregion

        #region On Movable Hit
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

        #region On Box Hit
        public virtual void OnHittedByBox(GameObject box)
        {
        }
        #endregion
    }
}
