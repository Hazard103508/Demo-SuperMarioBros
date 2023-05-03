using UnityEngine;
using UnityEngine.Events;

namespace UnityShared.Behaviours.PlayerAnimators
{
    public class Base3DPlayerAnimator : MonoBehaviour
    {
        public UnityEvent onLanding;
        public UnityEvent onFootstep;

        protected Animator _animator;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        protected virtual void Start()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        public void SetGrounded(bool grounded) => _animator.SetBool(_animIDGrounded, grounded);
        public void SetSpeed(float speed) => _animator.SetFloat(_animIDSpeed, speed);
        public void SetMotionSpeed(float motionSpeed) => _animator.SetFloat(_animIDMotionSpeed, motionSpeed);
        public void SetJump(bool jump) => _animator.SetBool(_animIDJump, jump);
        public void SetFreeFall(bool freeFall) => _animator.SetBool(_animIDFreeFall, freeFall);

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
                onLanding.Invoke();
        }
        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
                onFootstep.Invoke();
        }
    }
}