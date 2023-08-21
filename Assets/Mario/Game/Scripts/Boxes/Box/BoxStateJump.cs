using Mario.Application.Services;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Boxes.Box
{
    public class BoxStateJump : BoxState
    {
        #region Objects
        private Vector2 _initPosition;
        #endregion

        #region Constructor
        public BoxStateJump(Box box) : base(box)
        {
        }
        #endregion

        #region Protected Methods
        protected virtual void OnJumpCompleted()
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _initPosition = Box.transform.position;
            Box.Animator.SetTrigger("Jump");
            Box.Movable.Gravity = Box.Profile.FallSpeed;
            Box.Movable.MaxFallSpeed = Box.Profile.MaxFallSpeed;
            Box.Movable.AddJumpForce(Box.Profile.JumpAcceleration);
            Services.PoolService.GetObjectFromPool(Box.Profile.HitSoundFXPoolReference, Box.transform.position);
        }
        public override void Update()
        {
            if (Box.transform.position.y < _initPosition.y)
            {
                Box.transform.position = _initPosition;
                Box.StateMachine.TransitionTo(Box.StateMachine.StateIdle);
                OnJumpCompleted();
            }
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToTop(RayHitInfo hitInfo) => Box.HitObjects(hitInfo);
        #endregion
    }
}