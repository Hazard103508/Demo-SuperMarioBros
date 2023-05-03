using UnityEngine;
using UnityEngine.Events;
using UnityShared.Commons.Structs;

namespace UnityShared.Behaviours.Sprite
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteOutBoundsTrigger : MonoBehaviour
    {
        private Bounds<float> bordersOut;
        private SpriteRenderer spriteRenderer;

        public Bounds<bool> bordersToCheck;
        public Bounds<UnityEvent> onOut;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            var cam = Camera.main;
            var downLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

            var scale = transform.localScale;
            bordersOut = new Bounds<float>()
            {
                left = downLeft.x - (spriteRenderer.sprite.bounds.size.x * scale.x) / 2.0f,
                right = topRight.x + (spriteRenderer.sprite.bounds.size.x * scale.x) / 2.0f,
                bottom = downLeft.y - (spriteRenderer.sprite.bounds.size.y * scale.y) / 2.0f,
                top = topRight.y + (spriteRenderer.sprite.bounds.size.y * scale.y) / 2.0f
            };
        }

        void LateUpdate()
        {
            var pos = transform.position;
            if (bordersToCheck.left && pos.x < bordersOut.left)
                onOut.left.Invoke();
            else if (bordersToCheck.right && pos.x > bordersOut.right)
                onOut.right.Invoke();
            else if (bordersToCheck.bottom && pos.y < bordersOut.bottom)
                onOut.bottom.Invoke();
            else if (bordersToCheck.top && pos.y > bordersOut.top)
                onOut.top.Invoke();
        }
    }
}