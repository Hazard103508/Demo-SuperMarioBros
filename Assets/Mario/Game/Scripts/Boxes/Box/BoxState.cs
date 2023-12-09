using Mario.Commons.Structs;
using Mario.Game.Interfaces;
using Mario.Game.Player;

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

        #region Public Methods
        public void HitObjects(RayHitInfo hitInfo)
        {
            foreach (var obj in hitInfo.hitObjects)
            {
                var hitableObject = obj.Object.GetComponent<IHittableByBox>();
                hitableObject?.OnHittedByBox(Box.gameObject);
            }
        }
        #endregion

        #region On Movable Hit
        public virtual void OnHittedByMovingToTop(RayHitInfo hitInfo)
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
