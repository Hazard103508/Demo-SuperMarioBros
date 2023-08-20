using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

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
    }
}
