using UnityEngine;
using UnityEngine.Events;

namespace Mario.Game.Items
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ItemOutMapTrigger : MonoBehaviour
    {
        #region Objects
        private float bottomBorder;
        private SpriteRenderer spriteRenderer;
        #endregion

        #region Events
        public UnityEvent onOut;
        #endregion

        #region Unity Methods
        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            var cam = Camera.main;
            var downLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));

            var scale = transform.localScale;
            var spritePivot = 1 - spriteRenderer.sprite.pivot.y / spriteRenderer.sprite.rect.height;
            bottomBorder = downLeft.y - (spriteRenderer.sprite.bounds.size.y * scale.y) * spritePivot;
        }

        void LateUpdate()
        {
            var pos = transform.position;
            if (pos.y < bottomBorder)
                onOut.Invoke();
        }
        #endregion
    }
}