using UnityEngine;
using UnityEngine.Events;

namespace Mario.Game.Items
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ItemOutMapTrigger : MonoBehaviour
    {
        #region Objects
        [SerializeField] private Vector2 _margin;
        private float bottomBorder;

        private bool isOut;
        #endregion

        #region Events
        public UnityEvent onOutFromDown;
        public UnityEvent onOutFromLeft;
        public UnityEvent onOutFromRight;
        #endregion

        #region Unity Methods
        void Awake()
        {
            var cam = Camera.main;
            var downLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            bottomBorder = downLeft.y - _margin.y;
        }
        private void OnEnable()
        {
            isOut = false;
        }
        void LateUpdate()
        {
            if (isOut)
                return;

            var pos = transform.position;
            if (pos.y < bottomBorder)
            {
                onOutFromDown.Invoke();
                isOut = true;
            }
        }
        #endregion
    }
}