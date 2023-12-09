using Mario.Commons.Structs;
using Mario.Game.Interactable;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mario.Game.Npc.Koopa
{
    public abstract class KoopaState :
        IEnemy,
        IState,
        IHittableByMovingToLeft,
        IHittableByMovingToRight,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByBox,
        IHittableByKoppa,
        IHittableByFireBall
    {
        #region Properties
        protected Koopa Koopa { get; private set; }
        #endregion

        #region Constructor
        public KoopaState(Koopa koopa)
        {
            this.Koopa = koopa;
        }
        #endregion

        #region Public 
        public void Kill(Vector3 hitPosition)
        {
            Koopa.StateMachine.TransitionTo(Koopa.StateMachine.StateDead);
            ChangeSpeedAfferHit(hitPosition);
        }
        public virtual void OnGameUnfrozen() => Koopa.Movable.enabled = true;
        public virtual void OnGameFrozen() => Koopa.Movable.enabled = false;
        public void ChangeDirection()
        {
            Koopa.Renderer.flipX = !Koopa.Renderer.flipX;
            Koopa.Movable.Speed *= -1;
        }
        #endregion

        #region Protected Methods
        protected float GetDirection() => Koopa.Renderer.flipX ? -1 : 1;
        protected void ChangeDirectionToRight()
        {
            Koopa.Renderer.flipX = false;
            Koopa.Movable.Speed = Mathf.Abs(Koopa.Movable.Speed);
        }
        protected void ChangeDirectionToLeft()
        {
            Koopa.Renderer.flipX = true;
            Koopa.Movable.Speed = -Mathf.Abs(Koopa.Movable.Speed);
        }
        protected void ChangeSpeedAfferHit(Vector3 hitPosition)
        {
            if (Math.Sign(Koopa.Movable.Speed) != Math.Sign(Koopa.transform.position.x - hitPosition.x))
            {
                ChangeDirection();
            }
        }
        protected void HitObject(RayHitInfo hitInfo)
        {
            var removeHits = new List<HitObject>();
            foreach (var obj in hitInfo.hitObjects)
            {
                if (obj.Object.TryGetComponent<IHittableByKoppa>(out var hitableObject))
                {
                    removeHits.Add(obj);
                    hitableObject?.OnHittedByKoppa(Koopa);
                }
            }

            removeHits.ForEach(obj => hitInfo.hitObjects.Remove(obj));
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

        #region On Movable Hit
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

        #region On Koopa Hit
        public virtual void OnHittedByKoppa(Koopa koopa)
        {
        }
        #endregion

        #region On Fireball Hit
        public virtual void OnHittedByFireBall(Fireball fireball)
        {
        }
        #endregion
    }
}
