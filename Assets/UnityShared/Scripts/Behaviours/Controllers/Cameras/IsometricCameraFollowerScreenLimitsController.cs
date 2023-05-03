using System;
using UnityEngine;

namespace UnityShared.Behaviours.Controllers.Cameras
{
    public class IsometricCameraFollowerScreenLimitsController : MonoBehaviour
    {
        new public Camera camera;
        public GameObject targetToFollow;
        public Margins pixelMargins;

        private Margins viewportMargins;

        private void Start()
        {
            viewportMargins = new Margins()
            {
                left = pixelMargins.left,
                right = Screen.width - pixelMargins.right,
                bottom = pixelMargins.bottom,
                top = Screen.height - pixelMargins.top,
            };
        }
        private void LateUpdate()
        {
            var targetScreenPosition = camera.WorldToScreenPoint(targetToFollow.transform.position);
            if (targetScreenPosition.x < viewportMargins.left)
            {
                var leftMargin = camera.ScreenToWorldPoint(new Vector3(viewportMargins.left, targetScreenPosition.y, targetScreenPosition.z));
                var offset = transform.position - leftMargin;
                this.transform.position = targetToFollow.transform.position + offset;
            }
            else if (targetScreenPosition.x > viewportMargins.right)
            {
                var rightMargin = camera.ScreenToWorldPoint(new Vector3(viewportMargins.right, targetScreenPosition.y, targetScreenPosition.z));
                var offset = transform.position - rightMargin;
                this.transform.position = targetToFollow.transform.position + offset;
            }

            targetScreenPosition = camera.WorldToScreenPoint(targetToFollow.transform.position);
            if (targetScreenPosition.y < viewportMargins.bottom)
            {
                var bottomMargin = camera.ScreenToWorldPoint(new Vector3(targetScreenPosition.x, viewportMargins.bottom, targetScreenPosition.z));
                var offset = transform.position - bottomMargin;
                this.transform.position = targetToFollow.transform.position + offset;
            }
            else if (targetScreenPosition.y > viewportMargins.top)
            {
                var bottomMargin = camera.ScreenToWorldPoint(new Vector3(targetScreenPosition.x, viewportMargins.top, targetScreenPosition.z));
                var offset = transform.position - bottomMargin;
                this.transform.position = targetToFollow.transform.position + offset;
            }

            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -10);
        }

        [Serializable]
        public class Margins
        {
            public float left;
            public float right;
            public float top;
            public float bottom;
        }
    }
}