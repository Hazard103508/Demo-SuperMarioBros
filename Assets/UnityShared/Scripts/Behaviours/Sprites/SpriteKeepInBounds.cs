using UnityEngine;
using UnityEngine.Events;
using UnityShared.Commons.Structs;

namespace UnityShared.Behaviours.Sprite
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteKeepInBounds : MonoBehaviour
    {
        private Bounds<float> bordersIn;
        private SpriteRenderer spriteRenderer;

        public Bounds<bool> bordersToLimit;
        public Bounds<UnityEvent<BoundHit>> onHit;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            var cam = Camera.main;
            var downLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

            var scale = transform.localScale;
            bordersIn = new Bounds<float>()
            {
                left = downLeft.x + (spriteRenderer.sprite.bounds.size.x * scale.x) / 2.0f,
                right = topRight.x - (spriteRenderer.sprite.bounds.size.x * scale.x) / 2.0f,
                bottom = downLeft.y + (spriteRenderer.sprite.bounds.size.y * scale.y) / 2.0f,
                top = topRight.y - (spriteRenderer.sprite.bounds.size.y * scale.y) / 2.0f
            };
        }

        void LateUpdate()
        {
            var pos = transform.position;
            if (bordersToLimit.left && pos.x < bordersIn.left)
            {
                var hitInfo = new BoundHit()
                {
                    hitPoint = transform.position,
                    fixedPoint = new Vector3(bordersIn.left, transform.position.y)
                };

                transform.position = hitInfo.fixedPoint;
                onHit.left.Invoke(hitInfo);
            }
            else if (bordersToLimit.right && pos.x > bordersIn.right)
            {
                var hitInfo = new BoundHit()
                {
                    hitPoint = transform.position,
                    fixedPoint = new Vector3(bordersIn.right, transform.position.y)
                };

                transform.position = hitInfo.fixedPoint;
                onHit.right.Invoke(hitInfo);
            }
            else if (bordersToLimit.bottom && pos.y < bordersIn.bottom)
            {
                var hitInfo = new BoundHit()
                {
                    hitPoint = transform.position,
                    fixedPoint = new Vector3(transform.position.x, bordersIn.bottom)
                };

                transform.position = hitInfo.fixedPoint;
                onHit.bottom.Invoke(hitInfo);
            }
            else if (bordersToLimit.top && pos.y > bordersIn.top)
            {
                var hitInfo = new BoundHit()
                {
                    hitPoint = transform.position,
                    fixedPoint = new Vector3(transform.position.x, bordersIn.top)
                };

                transform.position = hitInfo.fixedPoint;
                onHit.top.Invoke(hitInfo);
            }
        }

        public class BoundHit
        {
            public Vector2 hitPoint;
            public Vector2 fixedPoint;
        }
    }
}