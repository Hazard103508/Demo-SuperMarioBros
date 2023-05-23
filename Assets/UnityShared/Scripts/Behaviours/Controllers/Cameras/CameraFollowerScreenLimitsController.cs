using System;
using UnityEngine;

namespace UnityShared.Behaviours.Controllers.Cameras
{
    public class CameraFollowerScreenLimitsController : MonoBehaviour
    {
        new public Camera camera;
        public GameObject targetToFollow;
        public Margins<bool> lockedMargin;
        public Margins<float> percentMargin;

        private Margins<float> pixelMargins;
        private Vector3 distance;
        private Margins<float> worldMargins;

        private void Start()
        {
            pixelMargins = new Margins<float>()
            {
                bottom = percentMargin.bottom * Screen.height,
                top = percentMargin.top * Screen.height,
                left = percentMargin.left * Screen.width,
                right = percentMargin.right * Screen.width,
            };

            distance = transform.position;
            worldMargins = new Margins<float>()
            {
                left = camera.ScreenToWorldPoint(Vector3.right * pixelMargins.left).x,
                right = camera.ScreenToWorldPoint(Vector3.right * (Screen.width - pixelMargins.right)).x,
                bottom = camera.ScreenToWorldPoint(Vector3.up * (pixelMargins.bottom)).y,
                top = camera.ScreenToWorldPoint(Vector3.up * (Screen.height - pixelMargins.top)).y,
            };
        }
        private void LateUpdate()
        {
            float x = this.transform.position.x - distance.x;
            float y = this.transform.position.y - distance.y;

            if (!lockedMargin.left && targetToFollow.transform.position.x - x < worldMargins.left)
                x = targetToFollow.transform.position.x - worldMargins.left;

            if (!lockedMargin.right && targetToFollow.transform.position.x - x > worldMargins.right)
                x = targetToFollow.transform.position.x - worldMargins.right;

            if (!lockedMargin.bottom && targetToFollow.transform.position.y - y < worldMargins.bottom)
                y = targetToFollow.transform.position.y - worldMargins.bottom;

            if (!lockedMargin.top && targetToFollow.transform.position.y - y > worldMargins.top)
                y = targetToFollow.transform.position.y - worldMargins.top;

            this.transform.position = Vector3.Lerp(this.transform.position, distance + new Vector3(x, y), Time.deltaTime * 8);
        }

        [Serializable]
        public class Margins<T>
        {
            public T left;
            public T right;
            public T top;
            public T bottom;
        }
    }
}