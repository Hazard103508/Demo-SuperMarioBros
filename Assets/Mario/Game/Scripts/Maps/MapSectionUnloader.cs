using UnityEngine;

namespace Mario.Game.Maps
{
    public class MapSectionUnloader : MonoBehaviour
    {
        #region Objects
        public int Width;
        #endregion

        #region Unity Methods
        void Update()
        {
            var viewportPoint = Camera.main.ViewportToWorldPoint(Vector3.zero);

            var position = transform.position + Vector3.right * this.Width;
            if (viewportPoint.x > position.x)
                Destroy(gameObject);
        }
        #endregion
    }
}