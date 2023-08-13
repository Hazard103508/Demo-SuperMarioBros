using Mario.Application.Services;
using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.ScriptableObjects.Interactable;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class Fireball : MonoBehaviour
    {
        #region Objects
        [SerializeField] private FireballProfile _profile;
        private Movable _movable;
        private readonly Bounds<RayHitInfo> _proximityBlock = new();
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
            _proximityBlock.left = new();
            _proximityBlock.right = new();
            _proximityBlock.bottom = new();
        }
        #endregion

        #region Public Methods
        public void ChangeDirectionToRight() => _movable.Speed = math.abs(_movable.Speed);
        public void ChangeDirectionToLeft() => _movable.Speed = -math.abs(_movable.Speed);
        #endregion

        #region Private Methods
        private void HitFromLeft(RayHitInfo hitInfo)
        {
            _proximityBlock.left = hitInfo;
            HitFromSide(hitInfo);
        }
        private void HitFromRight(RayHitInfo hitInfo)
        {
            _proximityBlock.right = hitInfo;
            HitFromSide(hitInfo);
        }
        private void HitFromSide(RayHitInfo hitInfo)
        {
            HitObject(hitInfo);

            if (hitInfo != null & hitInfo.IsBlock && gameObject.activeSelf)
            {
                Explode(hitInfo);
                PlayHitSound();
            }
        }
        private void HitFromBottom(RayHitInfo hitInfo)
        {
            _proximityBlock.bottom = hitInfo;
            HitObject(hitInfo);

            if (gameObject.activeSelf)
                Bounce();
        }
        private void Bounce()
        {
            if (_proximityBlock.bottom != null && _proximityBlock.bottom.IsBlock)
                _movable.AddJumpForce(_profile.BounceSpeed);
        }
        private void Explode(RayHitInfo hitInfo)
        {
            var explotion = Services.PoolService.GetObjectFromPool(_profile.ExplotionPoolReference);
            explotion.transform.position = hitInfo.hitObjects.First().Point;
            gameObject.SetActive(false);
        }
        private void HitObject(RayHitInfo hitInfo)
        {
            if (hitInfo.hitObjects.Any())
            {
                foreach (var obj in hitInfo.hitObjects)
                {
                    var hitableObject = obj.Object.GetComponent<IHitableByFireBall>();
                    if (hitableObject != null)
                    {
                        hitableObject.OnHittedByFireBall(this);
                        Explode(hitInfo);

                        return;
                    }
                }
            }
        }
        private void PlayHitSound() => Services.PoolService.GetObjectFromPool(_profile.HitSoundFXPoolReference);
        #endregion

        #region On local Ray Range Hit
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => HitFromRight(hitInfo);
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => HitFromLeft(hitInfo);
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => HitFromBottom(hitInfo);
        #endregion
    }
}