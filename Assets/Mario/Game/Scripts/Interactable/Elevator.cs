using Mario.Commons.Structs;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Interactable;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class Elevator : MonoBehaviour, IHittableByPlayerFromTop
    {
        #region Objects
        [SerializeField] private ElevatorProfile _profile;
        [SerializeField] private SpriteRenderer _renderer;
        private Bounds<float> borders;
        private float halfHeight;
        private PlayerController _playerOnTop;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            var cam = Camera.main;
            var downLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

            borders = new Bounds<float>()
            {
                bottom = downLeft.y,
                top = topRight.y,
            };

            halfHeight = _renderer.bounds.size.y / 2;
        }
        private void FixedUpdate()
        {
            transform.Translate(_profile.Speed * Time.deltaTime * Vector3.up);
            AttachPlayerPositionToElevator();
        }
        #endregion

        #region Public Methods
        public void OnOutScreenFromTop()
        {
            _playerOnTop = null;
            transform.position = new Vector3(transform.position.x, borders.bottom, transform.position.z);
        }
        public void OnOutScreenFromBottom()
        {
            _playerOnTop = null;
            transform.position = new Vector3(transform.position.x, borders.top, transform.position.z);
        }
        #endregion

        #region Private Methods
        private void AttachPlayerPositionToElevator()
        {
            if (_playerOnTop != null && _playerOnTop.Movable.JumpForce != 0)
                _playerOnTop = null;

            if (_playerOnTop != null)
            {
                _playerOnTop.Movable.SetJumpForce(0);
                _playerOnTop.Movable.SetNextYPosition(transform.position.y + halfHeight);
            }
        }
        #endregion

        #region IHittableByMovingToTop
        public void OnHittedByPlayerFromTop(PlayerController player)
        {
            _playerOnTop = player;
        }
        #endregion
    }
}