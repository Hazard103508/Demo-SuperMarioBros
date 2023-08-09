using Mario.Game.ScriptableObjects.Interactable;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class Fireball : MonoBehaviour
    {
        #region Objects
        [SerializeField] private FireballProfile _profile;
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
        private void Update()
        {
            _currentSpeed.y -= _profile.FallSpeed * Time.deltaTime;
            if (_currentSpeed.y < -_profile.MaxFallSpeed)
                _currentSpeed.y = -_profile.MaxFallSpeed;

            transform.Translate(_currentSpeed * Time.deltaTime, Space.World);
        }
        #endregion

        #region Private Methods
        private void Bounce(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                _currentSpeed.y = _profile.BounceSpeed;
        }
        #endregion

        #region On local Ray Range Hit
        //public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityBlock.left = hitInfo;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => Bounce(hitInfo);
        #endregion
    }
}