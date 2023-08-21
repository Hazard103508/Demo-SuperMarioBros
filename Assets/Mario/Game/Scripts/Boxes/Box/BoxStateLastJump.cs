using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Boxes.Box
{
    public class BoxStateLastJump : BoxState
    {
        #region Objects
        private Vector2 _initPosition;
        #endregion

        #region Constructor
        public BoxStateLastJump(Box box) : base(box)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _initPosition = Box.transform.position;
            Box.Animator.SetTrigger("LastJump");
            Box.Movable.Gravity = Box.Profile.FallSpeed;
            Box.Movable.MaxFallSpeed = Box.Profile.MaxFallSpeed;
            Box.Movable.AddJumpForce(Box.Profile.JumpAcceleration);
        }
        public override void Update()
        {
            if (Box.transform.position.y < _initPosition.y)
            {
                Box.transform.position = _initPosition;
                Box.StateMachine.TransitionTo(Box.StateMachine.StateDisable);
            }
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToTop(RayHitInfo hitInfo) => Box.HitObjects(hitInfo);
        #endregion
    }
}