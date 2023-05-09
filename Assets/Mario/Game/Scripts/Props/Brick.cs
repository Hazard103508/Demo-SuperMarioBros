using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects;
using UnityEngine;

namespace Mario.Game.Props
{
    public class Brick : MonoBehaviour, ITopHitable
    {
        public BrickProfile profile;

        private Vector3 _currentSpeed;
        private bool _isHitable;
        private float minY = 0.5f;

        private void Update()
        {
            CalculateGravity();
            Move();
        }

        public void HitTop(PlayerController player)
        {
            if (!_isHitable)
                return;

            _isHitable = false;
            _currentSpeed.y = profile.HitForce;
        }

        private void CalculateGravity()
        {
            if (transform.localPosition.y > minY)
                _currentSpeed.y -= profile.FallSpeed * Time.deltaTime;
        }
        private void Move()
        {
            var nextPosition = transform.localPosition + _currentSpeed * Time.deltaTime;

            if (nextPosition.y <= minY)
            {
                nextPosition.y = minY;
                _currentSpeed.y = 0;
                _isHitable = true;
            }

            transform.localPosition = nextPosition;
        }
    }
}