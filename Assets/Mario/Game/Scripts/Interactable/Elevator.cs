using Mario.Game.ScriptableObjects.Interactable;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Interactable
{
    public class Elevator : MonoBehaviour
    {
        #region Objects
        [SerializeField] private ElevatorProfile _profile;
        private Bounds<float> borders;
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
        }
        private void LateUpdate()
        {
            transform.Translate(_profile.Speed * Time.deltaTime * Vector3.up);
        }
        #endregion

        #region Public Methods
        public void OnOutScreenFromTop()
        {
            transform.position = new Vector3(transform.position.x, borders.bottom, transform.position.z);
        }
        public void OnOutScreenFromBottom()
        {
            transform.position = new Vector3(transform.position.x, borders.top, transform.position.z);
        }
        #endregion
    }
}