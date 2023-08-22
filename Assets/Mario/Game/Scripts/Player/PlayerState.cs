using Mario.Game.Interfaces;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public abstract class PlayerState :
        IState,
        IHittableByMovingToTop,
        IHittableByMovingToBottom,
        IHittableByMovingToLeft,
        IHittableByMovingToRight
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
        protected PlayerController Player { get; private set; }
        #endregion

        #region Constructor
        public PlayerState(PlayerController player)
        {
            Player = player;
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
    }
}
