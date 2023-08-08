using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class Fireball : MonoBehaviour
    {
        //public Vector2 _direction;
        public Vector3 _currentSpeed;
        public float _bounceForce;
        public float _fallSpeed;
        public float _maxFallSpeed;


        #region Unity Methods
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                _currentSpeed.y = 0;
                transform.localPosition = Vector3.up * 4.2f;
            }

            _currentSpeed.y -= _fallSpeed * Time.deltaTime;
            if (_currentSpeed.y < -_maxFallSpeed)
                _currentSpeed.y = -_maxFallSpeed;

            transform.Translate(_currentSpeed * Time.deltaTime, Space.World);
        }
        #endregion

        #region Private Methods
        private void Bounce()
        {
            print("REBOTE");
            _currentSpeed.y = _bounceForce;
        }
        #endregion

        #region On local Ray Range Hit
        //public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityBlock.left = hitInfo;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => Bounce();
        #endregion
    }
}