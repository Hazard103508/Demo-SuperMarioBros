using Mario.Commons.Structs;
using Mario.Game.Interactable;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using System;
using UnityEngine;

namespace Mario.Game.Npc.Goomba
{
    public abstract class GoombaState :
        IState,
        IEnemy,
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

        #region Properties
        protected Goomba Goomba { get; private set; }
        #endregion

        #region Constructor
        public GoombaState(Goomba goomba)
        {
            Goomba = goomba;
        }
        #endregion

        #region Public Methods
        public void Kill(Vector3 hitPosition)
        {
            Goomba.StateMachine.TransitionTo(Goomba.StateMachine.StateDead);
            ChangeSpeedAfferHit(hitPosition);
        }
        public virtual void OnGameUnfrozen() => Goomba.Movable.enabled = true;
        public virtual void OnGameFrozen() => Goomba.Movable.enabled = false;
        public void ChangeDirectionToRight()
        {
            Goomba.Renderer.flipX = true;
            Goomba.Movable.Speed = Mathf.Abs(Goomba.Movable.Speed);
        }
        public void ChangeDirectionToLeft()
        {
            Goomba.Renderer.flipX = false;
            Goomba.Movable.Speed = -Mathf.Abs(Goomba.Movable.Speed);
        }
        public void ChangeSpeedAfferHit(Vector3 hitPosition)
        {
            if (Math.Sign(Goomba.Movable.Speed) != Math.Sign(Goomba.transform.position.x - hitPosition.x))
                Goomba.Movable.Speed *= -1;
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
        public virtual void OnHittedByKoppa(Koopa.Koopa koopa)
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
