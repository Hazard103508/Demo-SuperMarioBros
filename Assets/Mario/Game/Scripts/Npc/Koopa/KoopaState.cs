using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public abstract class KoopaState :
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
        public virtual void OnHittedByPlayerFromTop(PlayerController_OLD player)
        {
        }
        public virtual void OnHittedByPlayerFromBottom(PlayerController_OLD player)
        {
        }
        public virtual void OnHittedByPlayerFromLeft(PlayerController_OLD player)
        {
        }
        public virtual void OnHittedByPlayerFromRight(PlayerController_OLD player)
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
