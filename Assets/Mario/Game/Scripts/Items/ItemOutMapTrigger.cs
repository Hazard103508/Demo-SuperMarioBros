using Mario.Commons.Structs;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Game.Items
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ItemOutMapTrigger : MonoBehaviour
    {
        #region Objects
        [SerializeField] private Bounds<ScreenEdge> borders;
        #endregion

        #region Unity Methods
        void LateUpdate()
        {
            var cam = Camera.main;
            var downLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

            if (ChechBottomEdge(downLeft.y))
                return;

            if (ChechTopEdge(topRight.y))
                return;

            if (ChechLeftEdge(downLeft.x))
                return;

            if (ChechRightEdge(topRight.x))
                return;
        }
        #endregion

        #region Private Methods
        private bool ChechBottomEdge(float screenEdge)
        {
            var border = borders.bottom;
            if (border.Check && transform.position.y < screenEdge - border.Margin)
            {
                border.onOut.Invoke();
                return true;
            }
            return false;
        }
        private bool ChechTopEdge(float edge)
        {
            var border = borders.top;
            if (border.Check && transform.position.y > edge + border.Margin)
            {
                border.onOut.Invoke();
                return true;
            }
            return false;
        }
        private bool ChechLeftEdge(float edge)
        {
            var border = borders.left;
            if (border.Check && transform.position.x < edge - border.Margin)
            {
                border.onOut.Invoke();
                return true;
            }
            return false;
        }
        private bool ChechRightEdge(float edge)
        {
            var border = borders.right;
            if (border.Check && transform.position.x > edge + border.Margin)
            {
                border.onOut.Invoke();
                return true;
            }
            return false;
        }
        #endregion

        #region Structures
        [Serializable]
        public class ScreenEdge
        {
            public bool Check;
            public float Margin;
            public UnityEvent onOut;
        }
        #endregion
    }
}