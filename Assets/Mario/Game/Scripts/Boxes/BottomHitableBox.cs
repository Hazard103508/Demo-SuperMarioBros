using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class BottomHitableBox : MonoBehaviour, IBottomHitable
    {
        [SerializeField] private AudioSource _hitSoundFX;
        private Animator _boxAnimator;
        protected bool IsHitable { get; set; }
        public bool IsJumping { get; private set; }

        protected virtual void Awake()
        {
            _boxAnimator = GetComponent<Animator>();
            IsHitable = true;
        }
        public virtual void OnHitFromBottom(PlayerController player)
        {
            if (!IsHitable)
                return;

            IsJumping = true;
            _boxAnimator.SetTrigger("Jump");
            IsHitable = false;
        }
        public virtual void OnJumpCompleted()
        {
            IsJumping = false;
        }
        protected void InstantiateContent(GameObject item)
        {
            var content = Instantiate(item);
            content.transform.position = this.transform.position;
        }
        protected void PlayHitSoundFX()
        {
            if (!_hitSoundFX.isPlaying)
                _hitSoundFX.Play();
        }
    }
}