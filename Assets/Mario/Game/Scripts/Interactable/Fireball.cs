using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Commons.Structs;
using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.ScriptableObjects.Interactable;
using System.Linq;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class Fireball : MonoBehaviour,
        IHittableByMovingToBottom,
        IHittableByMovingToLeft,
        IHittableByMovingToRight
    {
        #region Objects
        private IPoolService _poolService;
        private IPlayerService _playerService;

        private Movable _movable;
        [SerializeField] private FireballProfile _profile;
        #endregion

        #region Properties
        public Movable Movable => _movable;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();

            _movable = GetComponent<Movable>();
            _movable.Speed = _profile.Speed;
            _movable.Gravity = _profile.FallSpeed;
            _movable.MaxFallSpeed = _profile.MaxFallSpeed;
        }
        private void OnEnable()
        {
            _movable.SetJumpForce(0);
            _movable.ChekCollisions = true;
        }
        private void OnDisable() => _playerService.ReturnFireball();
        #endregion

        #region Public Methods
        public void ChangeDirectionToRight() => _movable.Speed = Mathf.Abs(_movable.Speed);
        public void ChangeDirectionToLeft() => _movable.Speed = -Mathf.Abs(_movable.Speed);
        public void OnOutOfScreen() => gameObject.SetActive(false);
        public Vector3 GetHitPosition() => gameObject.transform.position + new Vector3(-Mathf.Sign(Movable.Speed), 0);
        #endregion

        #region Private Methods
        private void HitBottomObject(RayHitInfo hitInfo)
        {
            HitObject(hitInfo);
            if (gameObject.activeSelf && hitInfo.IsBlock)
                _movable.SetJumpForce(_profile.BounceSpeed);
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
            _poolService.GetObjectFromPool(_profile.ExplotionPoolReference, hitInfo.hitObjects.First().Point);
            _movable.ChekCollisions = false;
            gameObject.SetActive(false);
        }
        private void PlayHitSound() => _poolService.GetObjectFromPool(_profile.HitSoundFXPoolReference, this.transform.position);
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToBottom(RayHitInfo hitInfo) => HitBottomObject(hitInfo);
        public void OnHittedByMovingToLeft(RayHitInfo hitInfo) => HitSideObject(hitInfo);
        public void OnHittedByMovingToRight(RayHitInfo hitInfo) => HitSideObject(hitInfo);
        #endregion

    }
}