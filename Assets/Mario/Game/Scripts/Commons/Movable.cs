using UnityEngine;

namespace Mario.Game.Commons
{
    public class Movable : MonoBehaviour
    {
        #region Objects
        private Vector2 _currentSpeed;
        private float _direction;
        #endregion

        #region Properties
        public float Gravity { get; set; }
        public float XSpeed
        {
            get => _currentSpeed.x;
            private set => _currentSpeed.x = value;
        }
        public float MaxFallSpeed { get; set; }
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
            //_proximityBlock.left = new();
            //_proximityBlock.right = new();
            //_proximityBlock.bottom = new();
        }
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