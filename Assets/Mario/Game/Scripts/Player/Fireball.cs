using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.ScriptableObjects.Interactable;
using System.Linq;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class Fireball : MonoBehaviour
    {
        #region Objects
        [SerializeField] private FireballProfile _profile;
        private readonly Bounds<RayHitInfo> _proximityBlock = new();
        private Vector3 _currentSpeed;
        private float _direction;
        #endregion

        #region Properties
        public float Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                _currentSpeed.x = _direction * Mathf.Abs(_currentSpeed.x);
            }
        }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _currentSpeed = _profile.Speed * Vector3.right;
        }
        private void LateUpdate()
        {
            Move();
        }
        private void OnEnable()
        {
            _currentSpeed.y = 0;
            _proximityBlock.left = new();
            _proximityBlock.right = new();
            _proximityBlock.bottom = new();
        }
        #endregion

        #region Private Methods
        private void Move()
        {
            _currentSpeed.y -= _profile.FallSpeed * Time.deltaTime;
            if (_currentSpeed.y < -_profile.MaxFallSpeed)
                _currentSpeed.y = -_profile.MaxFallSpeed;

            transform.Translate(_currentSpeed * Time.deltaTime, Space.World);
        }
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
                _currentSpeed.y = _profile.BounceSpeed;
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