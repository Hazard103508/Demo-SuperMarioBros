using Mario.Game.ScriptableObjects.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class Fireball : MonoBehaviour
    {
        [SerializeField] private FireballProfile _profile;
        private Vector3 _currentSpeed;


        #region Unity Methods
        private void Awake()
        {
            _currentSpeed = _profile.Speed * Vector3.right;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                _currentSpeed.y = 0;
                transform.localPosition = Vector3.up * 4.2f;
            }

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
            {
                print("REBOTE");
                _currentSpeed.y = _profile.BounceSpeed;
            }
        }
        #endregion

        #region On local Ray Range Hit
        //public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityBlock.left = hitInfo;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => Bounce(hitInfo);
        #endregion
    }
}