using UnityEngine;
using UnityEngine.Events;
using UnityShared.Commons.Structs;

namespace Mario.Game.Items
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ItemOutMapTrigger : MonoBehaviour
    {
        #region Objects
        [SerializeField] private Vector2 _margin;

        private Bounds<float> bordersOut;
        private bool isOut;
        #endregion

        #region Events
        public UnityEvent onOutFromDown;
        public UnityEvent onOutFromLeft;
        public UnityEvent onOutFromRight;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            isOut = false;
        }
        void LateUpdate()
        {
            if (isOut)
                return;

            var cam = Camera.main;
            var downLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

            bordersOut = new Bounds<float>()
            {
                left = downLeft.x - _margin.x,
                right = topRight.x + _margin.x,
                bottom = downLeft.y - _margin.y,
            };

            var pos = transform.position;

            if (pos.y < bordersOut.bottom)
            {
                onOutFromDown.Invoke();
                isOut = true;
                return;
            }

            if (pos.x < bordersOut.left)
            {
                onOutFromLeft.Invoke();
                isOut = true;
                return;
            }

            if (pos.x > bordersOut.right)
            {
                onOutFromRight.Invoke();
                isOut = true;
                return;
            }
        }
        #endregion
    }
}