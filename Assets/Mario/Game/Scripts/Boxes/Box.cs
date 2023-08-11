using Mario.Application.Services;
using Mario.Game.Enums;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Pool;
using System.Linq;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Boxes
{
    public class Box : MonoBehaviour, IHitableByPlayerFromBottom
    {
        #region Objects
        [SerializeField] private AudioSource _hitSoundFX;
        private Animator _boxAnimator;
        #endregion

        #region Properties
        protected bool IsHitable { get; set; }
        public bool IsJumping { get; private set; }
        #endregion

        #region Unity Methods
        protected virtual void Awake()
        {
            _boxAnimator = GetComponent<Animator>();
            IsHitable = true;
        }
        #endregion

        #region Protected Methods
        protected virtual void OnJumpCompleted()
        {
            IsJumping = false;
        }
        protected void ShowContent(PooledObjectProfile profile)
        {
            var obj = Services.PoolService.GetObjectFromPool(profile);
            obj.transform.position = this.transform.position;
        }
        protected void PlayHitSoundFX()
        {
            if (_hitSoundFX.enabled && !_hitSoundFX.isPlaying)
                _hitSoundFX.Play();
        }
        #endregion

        #region Private Methods
        private void HitObjectOnTop(RayHitInfo hitInfo)
        {
            if (!IsJumping)
                return;

            if (hitInfo.hitObjects.Any())
            {
                foreach (var obj in hitInfo.hitObjects)
                {
                    var hitableObject = obj.Object.GetComponent<IHitableByBox>();
                    hitableObject?.OnHittedByBox(this.gameObject);
                }
            }
        }
        #endregion

        #region On local Ray Range Hit
        public void OnProximityRayHitTop(RayHitInfo hitInfo) => HitObjectOnTop(hitInfo);
        #endregion

        #region On Player Hit
        public virtual void OnHittedByPlayerFromBottom(PlayerController player)
        {
            if (!IsHitable)
                return;

            IsJumping = true;
            _boxAnimator.SetTrigger("Jump");
            IsHitable = false;
        }
        #endregion
    }
}