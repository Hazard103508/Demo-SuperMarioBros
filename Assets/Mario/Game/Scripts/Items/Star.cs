using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Items
{
    public class Star : MonoBehaviour,
        IHittableByMovingToBottom,
        IHittableByMovingToLeft,
        IHittableByMovingToRight
    {
        #region Objects
        private IPoolService _poolService;

        private Movable _movable;
        [SerializeField] private StarProfile _profile;
        #endregion

        #region Properties
        public Movable Movable => _movable;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();

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
        #endregion

        #region Public Methods
        public void ChangeDirectionToRight() => _movable.Speed = Mathf.Abs(_movable.Speed);
        public void ChangeDirectionToLeft() => _movable.Speed = -Mathf.Abs(_movable.Speed);
        public void OnOutOfScreen() => gameObject.SetActive(false);
        #endregion

        #region Private Methods
        private void HitByPlayer(RayHitInfo hitInfo)
        {
            ///HitObject(hitInfo);
            ///if (gameObject.activeSelf && hitInfo.IsBlock)
            ///    _movable.SetJumpForce(_profile.BounceSpeed);
        }
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToBottom(RayHitInfo hitInfo) => HitByPlayer(hitInfo);
        public void OnHittedByMovingToLeft(RayHitInfo hitInfo) => HitByPlayer(hitInfo);
        public void OnHittedByMovingToRight(RayHitInfo hitInfo) => HitByPlayer(hitInfo);
        #endregion

    }
}