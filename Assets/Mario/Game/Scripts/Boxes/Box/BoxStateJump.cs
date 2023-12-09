using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Commons.Structs;
using UnityEngine;

namespace Mario.Game.Boxes.Box
{
    public class BoxStateJump : BoxState
    {
        #region Objects
        private readonly ISoundService _soundService;
        private Vector2 _initPosition;
        #endregion

        #region Constructor
        public BoxStateJump(Box box) : base(box)
        {
            _soundService = ServiceLocator.Current.Get<ISoundService>();
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
            Box.Renderer.sortingOrder = 1;
            Box.Animator.SetTrigger("Jump");
            Box.Movable.Gravity = Box.Profile.FallSpeed;
            Box.Movable.MaxFallSpeed = Box.Profile.MaxFallSpeed;
            Box.Movable.SetJumpForce(Box.Profile.JumpAcceleration);
            Box.Movable.ChekCollisions = true;
            _soundService.Play(Box.Profile.HitSoundFXPoolReference, Box.transform.position);
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
        public override void OnHittedByMovingToTop(RayHitInfo hitInfo) => HitObjects(hitInfo);
        #endregion
    }
}