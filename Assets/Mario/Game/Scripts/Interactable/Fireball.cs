using Mario.Application.Services;
using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.ScriptableObjects.Interactable;
using System.Linq;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Interactable
{
    public class Fireball : MonoBehaviour,
        IHittableByMovingToBottom,
        IHittableByMovingToLeft,
        IHittableByMovingToRight
    {
        #region Objects
        [SerializeField] private FireballProfile _profile;
        private Movable _movable;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _movable = GetComponent<Movable>();
            _movable.Speed = _profile.Speed;
            _movable.Gravity = _profile.FallSpeed;
            _movable.MaxFallSpeed = _profile.MaxFallSpeed;
        }
        private void OnEnable()
        {
            _movable.ChekCollisions = true;
        }
        #endregion

        #region Public Methods
        public void ChangeDirectionToRight() => _movable.Speed = Mathf.Abs(_movable.Speed);
        public void ChangeDirectionToLeft() => _movable.Speed = -Mathf.Abs(_movable.Speed);

        #endregion

        #region Private Methods
        private void HitBottomObject(RayHitInfo hitInfo)
        {
            HitObject(hitInfo);
            if (gameObject.activeSelf && hitInfo.IsBlock)
                _movable.AddJumpForce(_profile.BounceSpeed);
        }
        private void HitSideObject(RayHitInfo hitInfo)
        {
            HitObject(hitInfo);
            if (gameObject.activeSelf && hitInfo.IsBlock)
            {
                Explode(hitInfo);
                PlayHitSound();
            }
        }
        private void HitObject(RayHitInfo hitInfo)
        {
            foreach (var obj in hitInfo.hitObjects)
            {
                if (obj.Object.TryGetComponent<IHittableByFireBall>(out var hitableObject))
                {
                    hitableObject.OnHittedByFireBall(this);
                    Explode(hitInfo);
                    return;
                }
            }
        }
        private void Explode(RayHitInfo hitInfo)
        {
            Services.PoolService.GetObjectFromPool(_profile.ExplotionPoolReference, hitInfo.hitObjects.First().Point);
            _movable.ChekCollisions = false;
            gameObject.SetActive(false);
        }
        private void PlayHitSound() => Services.PoolService.GetObjectFromPool(_profile.HitSoundFXPoolReference, this.transform.position);
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToBottom(RayHitInfo hitInfo) => HitBottomObject(hitInfo);
        public void OnHittedByMovingToLeft(RayHitInfo hitInfo) => HitSideObject(hitInfo);
        public void OnHittedByMovingToRight(RayHitInfo hitInfo) => HitSideObject(hitInfo);
        #endregion

    }
}