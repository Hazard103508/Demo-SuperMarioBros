using Mario.Game.Interfaces;
using Mario.Game.Player;
using System.Linq;
using UnityEngine;
using UnityShared.Commons.Structs;

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

        #region On Ray Range Hit
        public void OnProximityRayHitTop(RayHitInfo hitInfo) => HitTopObjects(hitInfo);
        #endregion

        #region Private Methods
        private void HitTopObjects(RayHitInfo hitInfo)
        {
            if (!IsJumping)
                return;

            if (hitInfo.hitObjects.Any())
            {
                foreach (var obj in hitInfo.hitObjects)
                {
                    var hitableObject = obj.Object.GetComponent<IBottomHitableByBox>();
                    if (hitableObject != null)
                        hitableObject.OnHitFromBottomByBox(this.gameObject);
                }
            }
        }
        #endregion
    }
}