using UnityEngine;

namespace Mario.Game.Commons
{
    public class Movable : MonoBehaviour
    {
        #region Objects
        private Vector2 _currentSpeed;
        #endregion

        #region Properties
        public float Gravity { get; set; }
        public float Speed
        {
            get => _currentSpeed.x;
            set
            {
                _currentSpeed.x = value;
            }
        }
        public float MaxFallSpeed { get; set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _currentSpeed = Vector2.zero;
        }
        private void Update()
        {
            ApplyGravity();
        }
        private void LateUpdate()
        {
            Move();
        }
        private void OnEnable()
        {
            _currentSpeed.y = 0;
        }
        #endregion

        #region Public
        public void AddJumpForce(float force) => _currentSpeed.y = force;
        #endregion

        #region Private Methods
        private void ApplyGravity()
        {
            _currentSpeed.y -= Gravity * Time.deltaTime;
            if (_currentSpeed.y < -MaxFallSpeed)
                _currentSpeed.y = -MaxFallSpeed;
        }
        private void Move()
        {
            transform.Translate(_currentSpeed * Time.deltaTime, Space.World);
        }
        #endregion
    }
}