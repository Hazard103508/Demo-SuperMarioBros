using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityShared.Commons.Structs;

namespace Mario.Game.Boxes.Box
{
    public abstract class BoxState :
        IState,
        IHittableByMovingToTop,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight
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
        protected Box Box { get; private set; }
        #endregion

        #region Constructor
        public BoxState(Box box)
        {
            Box = box;
        }
        #endregion

        #region On Movable Hit
        public virtual void OnHittedByMovingToTop(RayHitInfo hitInfo)
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
    }
}
